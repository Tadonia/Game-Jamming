using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float acceleration = 2.0f;
    public float maxSpeed = 5.0f;
    public float maxJumpHeight = 7.0f;
    float speed;
    bool moving;
    bool isGrounded;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Deceleration();
        GetInputs();
        Movement();
        //Debug.Log(speed);
    }

    void Deceleration()
    {
        if (!moving) 
        {
            if (speed >= 0)
                speed = Mathf.Clamp(speed - acceleration * 2 * Time.deltaTime, 0, maxSpeed);
            else if (speed <= 0)
                speed = Mathf.Clamp(speed + acceleration * 2 * Time.deltaTime, -maxSpeed, 0);
        }
    }

    void GetInputs()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector2(0, maxJumpHeight), ForceMode2D.Impulse);
            isGrounded = false;
        }

        if (Input.GetKey("a"))
        {
            moving = true;
            speed = Mathf.Clamp(speed - acceleration * Time.deltaTime, -maxSpeed, 0);
        }
        else if (Input.GetKey("d"))
        {
            moving = true;
            speed = Mathf.Clamp(speed + acceleration * Time.deltaTime, 0, maxSpeed);
        }
        else { moving = false; }
    }

    void Movement()
    {
        Vector2 pos = transform.position;
        if (pos.x > -20 && pos.x < 20)
        {
            transform.position = new Vector2(pos.x + speed * Time.deltaTime, pos.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }
}
