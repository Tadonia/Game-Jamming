using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Sprite bulletSprite;
    public GameObject explosion;
    public Material material;

    public static float bulletSpeed;
    public static float fireRate;
    public static float bulletSize;
    public static float bulletDamage;

    float speedBonus = 0;
    float rateBonus = 0;
    float sizeBonus = 0;
    public static float damageBonus = 0;

    public Transform arm;
    Transform aimStart;

    LineRenderer line;
    Vector3 aimPos;

    float shootStartTime;
    GameObject bulletsParent;
    List<GameObject> bullets;

    public AudioSource shoot;
    public AudioSource hit;

    // Start is called before the first frame update
    void Start()
    {
        aimStart = arm.GetChild(0).transform;

        line = GetComponent<LineRenderer>();
        line.startWidth = 0.05f;
        line.endWidth = 0.05f;

        Cursor.visible = false;

        bulletsParent = new GameObject("Bullets");
        bullets = new List<GameObject>();

        damageBonus = 0;
    }

    // Update is called once per frame
    void Update()
    {
        AimLine();
        if (!GameOverManager.gameOver)
            MakeBullets();
        MoveBullets();
        DestroyBullets();
        AirBonus();
    }

    void AimLine()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        arm.right = new Vector3(mousePos.x, mousePos.y, 0) - transform.position;
        if (arm.right.x < 0)
        {
            arm.GetComponent<SpriteRenderer>().flipY = true;
            aimStart.localPosition = new Vector3(0.8f, -0.21f, 0);
        }
        else
        {
            arm.GetComponent<SpriteRenderer>().flipY = false;
            aimStart.localPosition = new Vector3(0.8f, 0.21f, 0);
        }

        Vector3 aimStartPos = arm.GetChild(0).position;

        aimPos = arm.GetChild(0).transform.right;
        
        RaycastHit2D hit = Physics2D.Raycast(aimStartPos, aimPos, 40, ~576);
        line.SetPosition(0, aimStartPos);
        if (hit.point != Vector2.zero)
            line.SetPosition(1, hit.point);
        else
            line.SetPosition(1, aimPos * 1000);
    }

    void MakeBullets()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Time.time > shootStartTime + 1f/(fireRate + rateBonus))
        {
            GameObject bullet = new GameObject("Bullet");
            bullet.transform.SetParent(bulletsParent.transform);

            bullet.AddComponent<SpriteRenderer>().sprite = bulletSprite;
            bullet.GetComponent<SpriteRenderer>().material = material;
            bullet.AddComponent<BoxCollider2D>().isTrigger = true;
            bullet.GetComponent<BoxCollider2D>().size = new Vector2(0.5f, 0.25f);

            Transform tran = bullet.transform;
            tran.position = arm.GetChild(0).transform.position;
            tran.localScale = new Vector3(bulletSize + sizeBonus, bulletSize + sizeBonus, 1);
            tran.right = aimPos;

            bullet.layer = 9;
            bullet.tag = "PlayerBullet";
            bullets.Add(bullet);
            shootStartTime = Time.time;

            shoot.Play();
        }
    }

    void MoveBullets()
    {
        if (bullets.Count != 0)
            foreach (GameObject bullet in bullets)
            {
                bullet.transform.Translate(new Vector3((bulletSpeed - speedBonus) * Time.deltaTime, 0, 0), bullet.transform);
            }
    }

    void DestroyBullets()
    {
        for (int i = bullets.Count - 1; i >= 0; i--)
        {
            Vector2 pos = bullets[i].transform.position;
            if (bullets[i].GetComponent<BoxCollider2D>().IsTouchingLayers(128 + 256) || pos.x > 20 || pos.x < -20 || pos.y > 10 || pos.y < -5)
            {
                Instantiate(explosion, bullets[i].transform.position, Quaternion.identity);
                Destroy(bullets[i]);
                bullets.RemoveAt(i);

                hit.Play();
            }
        }
    }

    void AirBonus()
    {
        if (PlayerController.airBonus)
        {
            if (PlayerController.isGrounded)
            {
                speedBonus = -0.6f;
                rateBonus = -0.4f;
                sizeBonus = -3.0f;
                damageBonus = -0.2f;
                Debug.Log("bad");
            }
            else
            {
                speedBonus = 0.3f;
                rateBonus = 0.2f;
                sizeBonus = 1.5f;
                damageBonus = 0.1f;
                Debug.Log("good");
            }
        }
    }

    public Transform GetArm()
    {
        return arm;
    }
}
