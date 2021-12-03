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
    public RectTransform curseMenu;
    public Image background;

    GameObject enemyParent;
    List<GameObject> enemies;

    bool levelingUp;
    float levelUpStartTime;
    bool cursing;
    float curseStartTime;

    bool nextWave;
    float waveStartTime;
    bool enemy1SpawnFinish = true;
    bool enemy2SpawnFinish = true;
    bool enemy3SpawnFinish = true;

    // Start is called before the first frame update
    void Start()
    {
        enemyParent = new GameObject("Enemies");
        waveNum = 0;
        enemies = new List<GameObject>();

        statsScreen.anchoredPosition = new Vector2(-841.5f, -70);
        levelScreen.anchoredPosition = new Vector2(774, -70);
        levelUpButton.anchoredPosition = new Vector2(246, -70);
        curseMenu.anchoredPosition = new Vector2(1600, 0);
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
        else if (waveNum == 1 && nextWave && Time.time > waveStartTime + 2)
        {
            Wave2();
            waveNum++;
            nextWave = false;
        }
        else if (waveNum == 2 && nextWave && Time.time > waveStartTime + 2)
        {
            Wave3();
            waveNum++;
            nextWave = false;
        }
        else if (waveNum == 3 && nextWave && Time.time > waveStartTime + 2)
        {
            Wave4();
            waveNum++;
            nextWave = false;
        }
        else if (waveNum == 4 && nextWave && Time.time > waveStartTime + 2)
        {
            Wave5();
            waveNum++;
            nextWave = false;
        }
        else if (waveNum == 5 && nextWave && Time.time > waveStartTime + 2)
        {
            Wave6();
            waveNum++;
            nextWave = false;
        }
        else if (waveNum == 6 && nextWave && Time.time > waveStartTime + 2)
        {
            Wave7();
            waveNum++;
            nextWave = false;
        }
        else if (waveNum == 7 && nextWave && Time.time > waveStartTime + 2)
        {
            Wave8();
            waveNum++;
            nextWave = false;
        }
        else if (waveNum >= 8 && nextWave && Time.time > waveStartTime + 2)
        {
            Wave9AndBeyond();
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
        for (int i = 0; i < 5; i++)
        {
            CreateEnemy1();
        }
        for (int i = 0; i < 5; i++)
        {
            CreateEnemy2();
        }
    }

    void Wave3()
    {
        for (int i = 0; i < 10; i++)
        {
            CreateEnemy3();
        }
        for (int i = 0; i < 4; i++)
        {
            CreateEnemy2();
        }
    }

    void Wave4()
    {
        for (int i = 0; i < 15; i++)
        {
            CreateEnemy2();
        }
    }

    void Wave5()
    {
        for (int i = 0; i < 10; i++)
        {
            CreateEnemy1();
        }
        for (int i = 0; i < 5; i++)
        {
            CreateEnemy3();
        }
    }

    void Wave6()
    {
        for (int i = 0; i < 10; i++)
        {
            CreateEnemy1();
        }
        for (int i = 0; i < 5; i++)
        {
            CreateEnemy2();
        }
        for (int i = 0; i < 5; i++)
        {
            CreateEnemy3();
        }
    }

    void Wave7()
    {
        for (int i = 0; i < 20; i++)
        {
            CreateEnemy3();
        }
    }

    void Wave8()
    {
        for (int i = 0; i < 25; i++)
        {
            CreateEnemy1();
        }
    }

    void Wave9AndBeyond()
    {
        CreateEnemy1();
        CreateEnemy2();
        CreateEnemy3();
        StartCoroutine(Wave9Enemy1());
        StartCoroutine(Wave9Enemy2());
        StartCoroutine(Wave9Enemy3());
    }

    IEnumerator Wave9Enemy1()
    {
        enemy1SpawnFinish = false;
        float spawnStartTime = Time.time;
        float rand = Random.Range(0, 0.2f);
        for (int i = 0; i < waveNum * 2 - 16; i++)
        {
            yield return null;
            if (Time.time > spawnStartTime + rand)
            {
                CreateEnemy1();
                spawnStartTime = Time.time;
                rand = Random.Range(0, 0.2f);
            }
            else --i;
        }
        enemy1SpawnFinish = true;
    }

    IEnumerator Wave9Enemy2()
    {
        enemy2SpawnFinish = false;
        float spawnStartTime = Time.time;
        float rand = Random.Range(0, 0.2f);
        for (int i = 0; i < waveNum * 1 - 11; i++)
        {
            yield return null;
            if (Time.time > spawnStartTime + rand)
            {
                CreateEnemy2();
                spawnStartTime = Time.time;
                rand = Random.Range(0, 0.2f);
            }
            else --i;
        }
        enemy2SpawnFinish = true;
    }

    IEnumerator Wave9Enemy3()
    {
        enemy3SpawnFinish = false;
        float spawnStartTime = Time.time;
        float rand = Random.Range(0, 0.2f);
        for (int i = 0; i < waveNum - 1; i++)
        {
            yield return null;
            if (Time.time > spawnStartTime + rand)
            {
                CreateEnemy3();
                spawnStartTime = Time.time;
                rand = Random.Range(0, 0.2f);
            }
            else --i;
        }
        enemy3SpawnFinish = true;
    }

    void CreateEnemy1()
    {
        int side = Random.Range(0, 2);
        side = side == 0 ? -20 : 20;
        float yRand = Random.Range(-0.5f, 5.0f);

        Vector2 pos = new Vector2(side, yRand);
        GameObject enemy = Instantiate(enemy1, pos, Quaternion.identity);
        enemy.GetComponent<Enemy1Controller>().damage += waveNum * 0.4f;
        enemy.GetComponent<Enemy1Controller>().health += waveNum * 0.2f;
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
        enemy.GetComponent<Enemy2Controller>().damage += waveNum * 0.3f;
        enemy.GetComponent<Enemy2Controller>().health += waveNum * 0.2f;
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
        enemy.GetComponent<Enemy3Controller>().damage += waveNum * 0.2f;
        enemy.GetComponent<Enemy3Controller>().health += waveNum * 0.2f;
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
                        ScoreManager.score += 100;
                    }
                }
                else if (enemies[i].TryGetComponent<Enemy2Controller>(out Enemy2Controller enemy2))
                {
                    if (enemy2.dead)
                    {
                        Destroy(enemies[i]);
                        enemies.RemoveAt(i);
                        ScoreManager.score += 120;
                    }
                }
                else if (enemies[i].TryGetComponent<Enemy3Controller>(out Enemy3Controller enemy3))
                {
                    if (enemy3.dead)
                    {
                        Destroy(enemies[i]);
                        enemies.RemoveAt(i);
                        ScoreManager.score += 120;
                    }
                }
            }
        }
    }

    void WaveComplete()
    {
        if (enemies.Count == 0 && !nextWave && !levelingUp && !cursing && enemy1SpawnFinish && enemy2SpawnFinish && enemy3SpawnFinish)
        {
            levelingUp = true;
            levelUpStartTime = Time.time;
            LevelUp.PointsUp();
            Cursor.visible = true;
            BothOrNeither.cursing = true;
            BothOrNeither.RandomCurse();
        }

        if (levelingUp)
        {
            statsScreen.anchoredPosition = Vector2.Lerp(new Vector2(-841.5f, -70), new Vector2(120, -70), (Time.time - levelUpStartTime) / 1);
            levelScreen.anchoredPosition = Vector2.Lerp(new Vector2(774, -70), new Vector2(-120, -70), (Time.time - levelUpStartTime) / 1);
            levelUpButton.anchoredPosition = Vector2.Lerp(new Vector2(246, -70), new Vector2(-120, 70), (Time.time - levelUpStartTime) / 1);
            background.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 0.5f), (Time.time - levelUpStartTime) / 1);
        }

        if (LevelUp.close)
        {
            LevelUp.close = false;
            levelingUp = false;
            cursing = true;
            curseStartTime = Time.time;

            statsScreen.anchoredPosition = new Vector2(-841.5f, -70);
            levelScreen.anchoredPosition = new Vector2(774, -70);
            levelUpButton.anchoredPosition = new Vector2(246, -70);
        }

        if (cursing)
        {
            curseMenu.anchoredPosition = Vector2.Lerp(new Vector2(1600, 0), new Vector2(0, 0), (Time.time - curseStartTime) / 1);
        }

        if (BothOrNeither.close)
        {
            BothOrNeither.close = false;
            curseMenu.anchoredPosition = new Vector2(1600, 0);
            background.color = new Color(0, 0, 0, 0);
            cursing = false;
            PlayerController.health = PlayerController.maxHealth;
            Cursor.visible = false;
            nextWave = true;
            waveStartTime = Time.time;
        }
    }
}
