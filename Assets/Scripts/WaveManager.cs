using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int waveNum;
    public GameObject enemy1;
    GameObject enemyParent;
    List<GameObject> enemies;

    // Start is called before the first frame update
    void Start()
    {
        enemyParent = new GameObject("Enemies");
        waveNum = 0;
        enemies = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (waveNum == 0)
        {
            Wave1();
            waveNum++;
        }
    }

    void Wave1()
    {
        for (int i = 0; i < 5; i++)
        {
            int side = Random.Range(0, 2);
            side = side == 0 ? -20 : 20;
            float yRand = Random.Range(-0.5f, 5.0f);

            Vector2 pos = new Vector2(side, yRand);
            GameObject enemy = Instantiate(enemy1, pos, Quaternion.identity);
            enemy.transform.parent = enemyParent.transform;

            enemies.Add(enemy);
        }
    }
}
