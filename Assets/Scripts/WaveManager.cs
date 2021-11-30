using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public int waveNum;
    public GameObject enemy1;
    GameObject enemyParent;
    List<GameObject> enemies;
    bool nextWave;
    float waveStartTime;

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
        if (waveNum == 1 && nextWave && Time.time > waveStartTime + 2)
        {
            Wave2();
            waveNum++;
            nextWave = false;
        }
        EnemyDeath();
        NextWave();
    }

    void Wave1()
    {
        for (int i = 0; i < 5; i++)
        {
            CreateEnemy1();
        }
    }

    void Wave2()
    {
        for (int i = 0; i < 10; i++)
        {
            CreateEnemy1();
        }
    }

    void CreateEnemy1()
    {
        int side = Random.Range(0, 2);
        side = side == 0 ? -20 : 20;
        float yRand = Random.Range(-0.5f, 5.0f);

        Vector2 pos = new Vector2(side, yRand);
        GameObject enemy = Instantiate(enemy1, pos, Quaternion.identity);
        enemy.transform.parent = enemyParent.transform;

        enemies.Add(enemy);
    }

    void EnemyDeath()
    {
        if (enemies.Count != 0)
        {
            for (int i = enemies.Count - 1; i >= 0; i--)
            {
                if (enemies[i].TryGetComponent<Enemy1Controller>(out Enemy1Controller enemy1))
                {
                    if (enemy1.dead)
                    {
                        Destroy(enemies[i]);
                        enemies.RemoveAt(i);
                    }
                }
            }
        }
    }

    void NextWave()
    {
        if (enemies.Count == 0 && !nextWave)
        {
            nextWave = true;
            waveStartTime = Time.time;
        }
    }
}
