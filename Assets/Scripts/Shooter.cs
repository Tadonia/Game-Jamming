using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public Sprite bulletSprite;
    public GameObject explosion;

    public float bulletSpeed = 7.0f;
    public float fireRate = 2.0f;
    public float bulletSize = 1.0f;

    public Transform arm;
    Transform aimStart;

    LineRenderer line;
    Vector3 aimPos;

    float shootStartTime;
    GameObject bulletsParent;
    List<GameObject> bullets;

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
    }

    // Update is called once per frame
    void Update()
    {
        AimLine();
        MakeBullets();
        MoveBullets();
        DestroyBullets();
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
        if (Input.GetKey(KeyCode.Mouse0) && Time.time > shootStartTime + 1f/fireRate)
        {
            GameObject bullet = new GameObject("Bullet");
            bullet.transform.SetParent(bulletsParent.transform);

            bullet.AddComponent<SpriteRenderer>().sprite = bulletSprite;
            bullet.AddComponent<BoxCollider2D>().isTrigger = true;

            Transform tran = bullet.transform;
            tran.position = arm.GetChild(0).transform.position;
            tran.right = aimPos;

            bullet.layer = 9;
            bullet.tag = "PlayerBullet";
            bullets.Add(bullet);
            shootStartTime = Time.time;
        }
    }

    void MoveBullets()
    {
        if (bullets.Count != 0)
            foreach (GameObject bullet in bullets)
            {
                bullet.transform.Translate(new Vector3(bulletSpeed * Time.deltaTime, 0, 0), bullet.transform);
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
            }
        }
    }

    public Transform GetArm()
    {
        return arm;
    }
}
