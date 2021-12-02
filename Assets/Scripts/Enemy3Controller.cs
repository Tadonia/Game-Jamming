using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3Controller : MonoBehaviour
{
    public float health = 5.0f;
    public float minSpeed = 3.0f;
    public float maxSpeed = 4.0f;
    public bool dead;
    public float damage = 1.0f;

    float speed = 5.0f;

    bool dashing;
    float dashStartTime;
    float dashDuration;
    Vector2 dashStartPos;
    Vector2 dashEndPos;

    Transform player;
    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        speed = Random.Range(minSpeed, maxSpeed);
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!dashing)
            MoveTowardsPlayer();
        DashTowardsPlayer();
        Die();
    }

    void MoveTowardsPlayer()
    {
        Vector2 pos = transform.position;
        Vector2 playerPos = player.position;
        if (pos.x > playerPos.x)
        {
            pos.x -= speed * Time.deltaTime;
            sprite.flipX = false;
        }
        else if (pos.x < playerPos.x)
        {
            pos.x += speed * Time.deltaTime;
            sprite.flipX = true;
        }

        transform.position = pos;
    }

    void DashTowardsPlayer()
    {
        Vector2 pos = transform.position;
        Vector2 playerPos = player.position;

        if ((pos.x < playerPos.x + 5 || pos.x > playerPos.x - 5) && !dashing)
        {
            dashing = true;
            dashStartTime = Time.time;
            dashStartPos = pos;
            //Vector2 dir = pos.y > playerPos.y ? new Vector2(0, -0.8f) : pos.y < playerPos.y ? new Vector2(0, 1.2f) : Vector2.zero;
            dashEndPos = pos + new Vector2(0, 0.2f) + (playerPos - pos).normalized * 10;
            dashDuration = Vector2.Distance(dashStartPos, dashEndPos) / (speed * 2);
        }

        if (dashing)
        {
            float t = (Time.time - dashStartTime) / dashDuration;

            if (dashStartPos != dashEndPos)
                transform.position = Vector2.Lerp(dashStartPos, dashEndPos, 1 - Mathf.Pow(1 - t, 3));
        }

        if (Time.time > dashStartTime + 0.8f)
            dashing = false;
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
            health -= 1;
        }
    }
}
