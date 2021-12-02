using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static float maxHealth;
    public static float defence;
    public static float damageReduction;
    public static float acceleration;
    public static float maxSpeed;
    public static float maxJumpHeight;
    public static float dashSpeed;
    public static float immunityTime;

    public static float health;
    float hitStartTime;
    float speed;

    bool moving;
    bool isGrounded;

    bool dashing;
    char lastDashInput;
    float dashTimer;

    Rigidbody2D rb;
    Animator animator;
    Image healthBar;
    Text healthBarNumber;
    ParticleSystem afterimages;
    ParticleSystemRenderer afterimagesRenderer;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        healthBar = GameObject.FindGameObjectWithTag("HealthBar").transform.GetChild(1).GetComponent<Image>();
        healthBarNumber = GameObject.FindGameObjectWithTag("HealthBar").transform.GetComponentInChildren<Text>();
        afterimages = GetComponent<ParticleSystem>();
        afterimagesRenderer = GetComponent<ParticleSystemRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Deceleration();
        GetInputs();
        Movement();
        AfterImages();
        ChangeHealthBar();
        animator.SetFloat("MoveX", speed * 1.5f);
        //Debug.Log(speed);
        //Debug.Log(dashing);
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

        /*if (speed > -0.1f && speed < 0.1f)
        {
            animator.speed = 0;
        }
        else { animator.speed = 1; }*/

        animator.speed = Mathf.Abs(speed) / (maxSpeed - 1);
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

    void AfterImages()
    {
        if (dashing && afterimages.isStopped)
        {
            afterimages.Play();
            if (speed > 0) afterimagesRenderer.flip = Vector3.zero;
            else if (speed < 0) afterimagesRenderer.flip = Vector3.right;
        }
        else if (!dashing)
        {
            afterimages.Stop();
        }
    }

    void ChangeHealthBar()
    {
        healthBar.fillAmount = health / maxHealth;
        healthBarNumber.text = decimal.Round((decimal)health, 2) + "/" + decimal.Round((decimal)maxHealth, 2);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("EnemyBullet"))
        {
            float damage = 0;
            if (collision.TryGetComponent<Enemy1Controller>(out Enemy1Controller enemy1))
            {
                damage = enemy1.damage;
            }
            else if (collision.TryGetComponent<Enemy2Controller>(out Enemy2Controller enemy2))
            {
                damage = enemy2.damage;
            }
            else if (collision.TryGetComponent<Enemy3Controller>(out Enemy3Controller enemy3))
            {
                damage = enemy3.damage;
            }
            damage = damage / damageReduction - defence;

            if (Time.time > hitStartTime + immunityTime)
            {
                health -= damage;
                hitStartTime = Time.time;
            }
        }
    }
}
