using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    public int waveNum;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;

    public RectTransform statsScreen;
    public RectTransform levelScreen;
    public RectTransform levelUpButton;
    public Image background;

    GameObject enemyParent;
    List<GameObject> enemies;

    bool levelingUp;
    float levelUpStartTime;

    bool nextWave;
    float waveStartTime;

    // Start is called before the first frame update
    void Start()
    {
        enemyParent = new GameObject("Enemies");
        waveNum = 0;
        enemies = new List<GameObject>();

        statsScreen.anchoredPosition = new Vector2(-841.5f, -70);
        levelScreen.anchoredPosition = new Vector2(774, -70);
        levelUpButton.anchoredPosition = new Vector2(246, -70);
        background.color = new Color(0, 0, 0, 0);
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
        WaveComplete();
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
        for (int i = 0; i < 15; i++)
        {
            CreateEnemy3();
        }
        for (int i = 0; i < 5; i++)
        {
            CreateEnemy2();
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

    void CreateEnemy2()
    {
        int side = Random.Range(0, 2);
        side = side == 0 ? -20 : 20;
        float yRand = Random.Range(-0.5f, -0.5f);

        Vector2 pos = new Vector2(side, yRand);
        GameObject enemy = Instantiate(enemy2, pos, Quaternion.identity);
        enemy.transform.parent = enemyParent.transform;

        enemies.Add(enemy);
    }

    void CreateEnemy3()
    {
        int side = Random.Range(0, 2);
        side = side == 0 ? -20 : 20;
        float yRand = Random.Range(-5.0f, 5.0f);

        Vector2 pos = new Vector2(side, yRand);
        GameObject enemy = Instantiate(enemy3, pos, Quaternion.identity);
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
                else if (enemies[i].TryGetComponent<Enemy2Controller>(out Enemy2Controller enemy2))
                {
                    if (enemy2.dead)
                    {
                        Destroy(enemies[i]);
                        enemies.RemoveAt(i);
                    }
                }
                else if (enemies[i].TryGetComponent<Enemy3Controller>(out Enemy3Controller enemy3))
                {
                    if (enemy3.dead)
                    {
                        Destroy(enemies[i]);
                        enemies.RemoveAt(i);
                    }
                }
            }
        }
    }

    void WaveComplete()
    {
        if (enemies.Count == 0 && !nextWave && !levelingUp)
        {
            levelingUp = true;
            levelUpStartTime = Time.time;
            LevelUp.PointsUp();
        }

        if (levelingUp)
        {
            statsScreen.anchoredPosition = Vector2.Lerp(new Vector2(-841.5f, -70), new Vector2(120, -70), (Time.time - levelUpStartTime) / 1);
            levelScreen.anchoredPosition = Vector2.Lerp(new Vector2(774, -70), new Vector2(-120, -70), (Time.time - levelUpStartTime) / 1);
            levelUpButton.anchoredPosition = Vector2.Lerp(new Vector2(246, -70), new Vector2(-120, 70), (Time.time - levelUpStartTime) / 1);
            background.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 0.5f), (Time.time - levelUpStartTime) / 1);
        }

        if (LevelUp.leveledUp)
        {
            LevelUp.leveledUp = false;
            statsScreen.anchoredPosition = new Vector2(-841.5f, -70);
            levelScreen.anchoredPosition = new Vector2(774, -70);
            levelUpButton.anchoredPosition = new Vector2(246, -70);
            background.color = new Color(0, 0, 0, 0);

            levelingUp = false;
            nextWave = true;
            waveStartTime = Time.time;
        }
    }
}
