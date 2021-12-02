using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Controller : MonoBehaviour
{
    public float health = 5.0f;
    public float damage = 1.0f;

    public float maxSpeedX;
    public float minRangeX = 5.0f;
    public float maxRangeX = 8.0f;

    public float minRangeY = 10.0f;
    public float maxRangeY = 17.0f;

    public float minDelay = 0.0f;
    public float maxDelay = 3.0f;

    public bool dead;
    bool jumping;
    bool turning;

    float speedX;
    public float accelerationX = 7.0f;
    float speedY;
    public float accelerationY = 20.0f;

    Transform player;
    //SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        maxSpeedX = Random.Range(minRangeX, maxRangeX);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        speedX = maxSpeedX;

        Vector2 pos = transform.position;
        Vector2 playerPos = player.position;

        //sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Acceleration();
        MoveTowardsPlayer();
        Bounce();
        Die();
    }

    void Acceleration()
    {
        Vector2 pos = transform.position;
        Vector2 playerPos = player.position;
        if (pos.x > playerPos.x) speedX -= accelerationX * Time.deltaTime;
        else if (pos.x < playerPos.x) speedX += accelerationX * Time.deltaTime;
        speedX = Mathf.Clamp(speedX, -maxSpeedX, maxSpeedX);
    }

    void MoveTowardsPlayer()
    {
        Vector2 pos = transform.position;
        Vector2 playerPos = player.position;
        transform.position = new Vector3(pos.x + speedX * Time.deltaTime, pos.y);
    }

    void Bounce()
    {
        if (!jumping)
        {
            jumping = true;
            speedY = Random.Range(minRangeY, maxRangeY);
            float delay = Random.Range(minDelay, maxDelay);
            IEnumerator coroutine = Bouncing(delay);
            StartCoroutine(coroutine);
        }

    }

    IEnumerator Bouncing(float delay)
    {
        float limit = -speedY;
        yield return new WaitForSeconds(delay);

        while (speedY > limit)
        {
            yield return new WaitForSeconds(Time.deltaTime);
            transform.position = new Vector3(transform.position.x, transform.position.y + speedY * Time.deltaTime);
            speedY -= accelerationY * Time.deltaTime;
        }
        transform.position = new Vector3(transform.position.x, -0.5f);
        jumping = false;    

    }

    void Die()
    {
        if (health <= 0)
        {
            dead = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            health -= Shooter.bulletDamage;
        }
    }
}
