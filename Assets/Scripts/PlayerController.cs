using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float acceleration = 2.0f;
    public float maxSpeed = 5.0f;
    public float maxJumpHeight = 7.0f;
    public float dashSpeed = 10.0f;

    float speed;

    bool moving;
    bool isGrounded;
    bool dashing;
    char lastDashInput;

    float dashTimer;

    Rigidbody2D rb;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Deceleration();
        GetInputs();
        Movement();
        animator.SetFloat("MoveX", speed);
        //Debug.Log(speed);
    }

    void Deceleration()
    {
        if (!moving) 
        {
            if (!dashing)
            {
                if (speed >= 0)
                    speed = Mathf.Clamp(speed - acceleration * 2 * Time.deltaTime, 0, maxSpeed);
                else if (speed <= 0)
                    speed = Mathf.Clamp(speed + acceleration * 2 * Time.deltaTime, -maxSpeed, 0);
            }
            else
            {
                if (speed >= 0)
                    speed = speed - acceleration * 2 * Time.deltaTime;
                else if (speed <= 0)
                    speed = speed + acceleration * 2 * Time.deltaTime;
            }
        }
    }

    void GetInputs()
    {
        if (Input.GetButton("Jump") && isGrounded)
        {
            rb.AddForce(new Vector2(0, maxJumpHeight), ForceMode2D.Impulse);
            isGrounded = false;
        }

        if (Input.GetKey("a"))
        {
            moving = true;
            if (!dashing)
                speed = Mathf.Clamp(speed - acceleration * Time.deltaTime, -maxSpeed, 0);
            else
                speed = speed - acceleration * Time.deltaTime;

            if (transform.position.x >= 20)
            {
                transform.position = new Vector2(20, transform.position.y);
            }
        }
        else if (Input.GetKey("d"))
        {
            moving = true;
            if (!dashing)
                speed = Mathf.Clamp(speed + acceleration * Time.deltaTime, 0, maxSpeed);
            else
                speed = speed + acceleration * Time.deltaTime;

            if (transform.position.x <= -20)
            {
                transform.position = new Vector2(-20, transform.position.y);
            }
        }
        else { moving = false; }

        if (Input.GetKeyDown("a"))
        {
            if (Time.time < dashTimer + 0.3f && lastDashInput == 'a')
            {
                speed = -dashSpeed;
                dashing = true;
            }
            else 
            { 
                dashTimer = Time.time;
                lastDashInput = 'a';
            }
        }
        else if (Input.GetKeyDown("d"))
        {
            if (Time.time < dashTimer + 0.3f && lastDashInput == 'd')
            {
                speed = dashSpeed;
                dashing = true;
            }
            else
            {
                dashTimer = Time.time;
                lastDashInput = 'd';
            }
        }

        if (Time.time >= dashTimer + 0.3f)
        {
            dashing = false;
        }
    }

    void Movement()
    {
        Vector2 pos = transform.position;
        if (pos.x >= -20 && pos.x <= 20)
        {
            transform.position = new Vector2(pos.x + speed * Time.deltaTime, pos.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }
}
