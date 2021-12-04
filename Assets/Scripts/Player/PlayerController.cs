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
    public static int maxJumps;

    public static float health;
    float hitStartTime;
    float speed;

    bool moving;
    public static bool isGrounded;
    float jumpNum;

    public static bool airBonus;
    bool inAir;
    public static float healthBonus = 0;
    float defenceBonus = 0;
    float speedBonus = 0;
    float dashSpeedBonus = 0;

    bool dashing;
    char lastDashInput;
    float dashTimer;

    Rigidbody2D rb;
    Animator animator;
    Image healthBar;
    Text healthBarNumber;
    ParticleSystem afterimages;
    ParticleSystemRenderer afterimagesRenderer;
    SpriteRenderer sprite;
    SpriteRenderer armSprite;

    public AudioSource jump;
    public AudioSource hurt;
    public AudioSource dash;

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
        sprite = GetComponent<SpriteRenderer>();
        armSprite = GetComponent<Shooter>().GetArm().gameObject.GetComponent<SpriteRenderer>();

        isGrounded = false;
        airBonus = false;
        healthBonus = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Deceleration();
        GetInputs();
        Movement();
        Jump();
        AfterImages();
        ChangeHealthBar();
        AirBonus();
        GameOver();
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
                    speed = Mathf.Clamp(speed - acceleration * 2 * Time.deltaTime, 0, maxSpeed + speedBonus);
                else if (speed <= 0)
                    speed = Mathf.Clamp(speed + acceleration * 2 * Time.deltaTime, -maxSpeed - speedBonus, 0);
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

        animator.speed = Mathf.Abs(speed) / (Mathf.Clamp((maxSpeed + speedBonus), 1.1f, maxSpeed + speedBonus) - 1);
    }

    void GetInputs()
    {
        if (Input.GetKey("a"))
        {
            moving = true;
            if (!dashing)
                speed = Mathf.Clamp(speed - acceleration * Time.deltaTime, -maxSpeed - speedBonus, 0);
            else
                speed = speed - acceleration * Time.deltaTime;
        }
        else if (Input.GetKey("d"))
        {
            moving = true;
            if (!dashing)
                speed = Mathf.Clamp(speed + acceleration * Time.deltaTime, 0, maxSpeed + speedBonus);
            else
                speed = speed + acceleration * Time.deltaTime;
        }
        else { moving = false; }

        if (Input.GetKeyDown("a"))
        {
            if (Time.time < dashTimer + 0.3f && lastDashInput == 'a')
            {
                speed = -dashSpeed - dashSpeedBonus;
                dashing = true;
                dash.Play();
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
                speed = dashSpeed + dashSpeedBonus;
                dashing = true;
                dash.Play();
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

        if (transform.position.x <= -20)
        {
            transform.position = new Vector2(-20, transform.position.y);
        }

        if (transform.position.x >= 20)
        {
            transform.position = new Vector2(20, transform.position.y);
        }
    }

    void Jump()
    {
        if (jumpNum > maxJumps - 1)
        {
            isGrounded = false;
        }
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(new Vector2(0, maxJumpHeight), ForceMode2D.Impulse);
            jumpNum += 1;
            inAir = true;
            jump.Play();
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
        healthBar.fillAmount = health / (maxHealth + healthBonus);
        healthBarNumber.text = decimal.Round((decimal)health, 2) + "/" + decimal.Round((decimal)(maxHealth + healthBonus), 2);
    }

    void AirBonus()
    {
        if (airBonus)
        {
            if (!inAir)
            {
                healthBonus = -3.0f;
                defenceBonus = -0.4f;
                speedBonus = -0.4f;
                dashSpeedBonus = -0.6f;
            }
            else
            {
                healthBonus = 1.5f;
                defenceBonus = 0.2f;
                speedBonus = 0.2f;
                dashSpeedBonus = 0.3f;
            }
        }
    }

    void GameOver()
    {
        if (GameOverManager.gameOver)
        {
            transform.position = new Vector3(transform.position.x, -50, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
        inAir = false;
        jumpNum = 0;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("EnemyBullet"))
        {
            if (Time.time > hitStartTime + immunityTime)
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
                damage = damage / damageReduction - defence - defenceBonus;

                if (damage < 0) { damage = 0; }
                health -= damage;
                hitStartTime = Time.time;
                IEnumerator knockback = Knockback(collision.transform.position);
                StartCoroutine(HitFlash());
                StartCoroutine(knockback);
                hurt.Play();
            }
        }
    }

    IEnumerator Knockback(Vector2 enemy)
    {
        float knockbackStartTime = Time.time;
        Vector2 pos = transform.position;
        Vector2 dir = (pos - enemy).normalized;
        if (pos != enemy)
        {
            while (Time.time < knockbackStartTime + 0.1f)
            {
                yield return null;
                pos.x += dir.x * Time.deltaTime * 10;
                if (pos.y > -0.4f)
                    pos.y += dir.y * Time.deltaTime * 10;
                transform.position = pos;
            }
        }
    }

    IEnumerator HitFlash()
    {
        int dir = 1;
        float flashStartTime = Time.time;
        while (Time.time < hitStartTime + immunityTime)
        {
            yield return null;
            if (Time.time < flashStartTime + 0.1f)
            {
                sprite.color = new Color(1, sprite.color.g - dir * (Time.deltaTime / 0.15f), sprite.color.b - dir * (Time.deltaTime / 0.15f));
                armSprite.color = new Color(1, sprite.color.g - dir * (Time.deltaTime / 0.15f), sprite.color.b - dir * (Time.deltaTime / 0.15f));
                //Debug.Log("here");
            }
            else
            {
                dir = -dir;
                flashStartTime = Time.time;
            }
        }
        sprite.color = new Color(1, 1, 1);
        armSprite.color = new Color(1, 1, 1);
    }
}
