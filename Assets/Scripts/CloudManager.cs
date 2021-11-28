using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudManager : MonoBehaviour
{
    public Sprite[] cloudSprites;
    public int numberOfClouds = 20;
    public float speed = 2.0f;
    GameObject[] clouds;

    // Start is called before the first frame update
    void Start()
    {
        clouds = new GameObject[numberOfClouds];

        for (int i = 0; i < numberOfClouds; i++)
        {
            GameObject cloud = new GameObject("Cloud");
            cloud.AddComponent<SpriteRenderer>().sortingOrder = -10;
            cloud.transform.parent = transform;

            MakeCloud(cloud);

            clouds[i] = cloud;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < clouds.Length; i++)
        {
            Vector2 pos = clouds[i].transform.position;
            float height = Camera.main.orthographicSize + 2;
            float scale = 1.0f - (height - pos.y) / height;

            pos.x -= speed * scale * Time.deltaTime;
            if (pos.x <= -22.0f)
            {
                MakeCloud(clouds[i]);
                pos.x = 22.0f;
            }
            else if (pos.x >= 22.0f)
            {
                MakeCloud(clouds[i]);
                pos.x = -22.0f;
            }
            pos.y = clouds[i].transform.position.y;

            clouds[i].transform.position = pos;
        }
    }

    void MakeCloud(GameObject cloud)
    {
        int rand = Random.Range(0, cloudSprites.Length);
        cloud.GetComponent<SpriteRenderer>().sprite = cloudSprites[rand];

        float height = Camera.main.orthographicSize + 2;
        float randX = Random.Range(-20.0f, 20.0f);
        float randY = Random.Range(2.0f, height);
        cloud.transform.position = new Vector2(randX, randY);

        float scale = 1.0f - (height - cloud.transform.position.y) / height;
        cloud.transform.localScale = new Vector3(scale, scale, 1);

        cloud.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, scale);
    }
}
