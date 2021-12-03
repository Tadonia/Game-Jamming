using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1Controller : MonoBehaviour
{
    public float health = 5.0f;
    public float minSpeed = 3.0f;
    public float maxSpeed = 4.0f;
    public bool dead;
    public float damage = 1.0f;

    float speed = 5.0f;

    bool dashing;
    bool spinning;
    float spinSpeed;
    float spinStartTime;
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
        if (!dashing && !spinning)
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

        if (Vector2.Distance(pos, playerPos) <= 5.5f && !dashing && !spinning)
        {
            spinning = true;
            spinStartTime = Time.time;
        }

        if (spinning)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.rotation.eulerAngles.z + spinSpeed * Time.deltaTime));
            if (Time.time < spinStartTime + 2)
                spinSpeed += 720 * Time.deltaTime;
            else
            {
                dashing = true;
                spinning = false;
                dashStartTime = Time.time;
                dashStartPos = pos;
                dashEndPos = playerPos;
                dashDuration = Vector2.Distance(dashStartPos, dashEndPos) / (speed * 2);
            }
        }

        if (dashing)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, transform.rotation.eulerAngles.z + spinSpeed * Time.deltaTime));
            float t = (Time.time - dashStartTime) / dashDuration;
            if (dashStartPos != dashEndPos)
                transform.position = Vector2.Lerp(dashStartPos, dashEndPos, t);
            spinSpeed -= 720 * Time.deltaTime;
            if (t >= 1)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                spinSpeed = 0;
                dashing = false;
            }
        }
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
            health -= Shooter.bulletDamage + Shooter.damageBonus;
        }
    }
}
