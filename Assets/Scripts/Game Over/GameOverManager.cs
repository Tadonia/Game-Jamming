using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverManager : MonoBehaviour
{
    public static bool gameOver;

    public Image screen;
    public Image button;
    public Image background;

    public Text score;
    public Text time;
    public Text wave;
    public Text curse;
    public Text final;

    public Text highTime;
    public Text highWave;
    public Text highScore;

    bool isOver;
    float gameOverStartTime;
    bool flashing;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        screen.color = new Color(1, 1, 1, 0);
        button.color = new Color(1, 1, 1, 0);
        background.color = new Color(0, 0, 0, 0);

        score.color = new Color(1, 1, 1, 0);
        time.color = new Color(1, 1, 1, 0);
        wave.color = new Color(1, 1, 1, 0);
        curse.color = new Color(1, 1, 1, 0);
        final.color = new Color(1, 1, 1, 0);

        highTime.color = new Color(1, 1, 1, 0);
        highWave.color = new Color(1, 1, 1, 0);
        highScore.color = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerController.health <= 0)
        {
            gameOver = true;
            if (!isOver)
            {
                isOver = true;
                gameOverStartTime = Time.time;
                Cursor.visible = true;
            }
        }
        if (isOver)
        {
            screen.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), (Time.time - gameOverStartTime) / 1);
            button.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), (Time.time - gameOverStartTime) / 1);
            background.color = Color.Lerp(new Color(1, 0, 0, 0), new Color(0, 0, 0, 1), (Time.time - gameOverStartTime) / 5);

            score.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), (Time.time - gameOverStartTime - 1.5f) / 0.5f);
            time.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), (Time.time - gameOverStartTime - 2f) / 0.5f);
            wave.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), (Time.time - gameOverStartTime - 2.5f) / 0.5f);
            curse.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), (Time.time - gameOverStartTime - 3f) / 0.5f);
            final.color = Color.Lerp(new Color(1, 1, 1, 0), new Color(1, 1, 1, 1), (Time.time - gameOverStartTime - 3.5f) / 0.5f);

            if (Time.time > gameOverStartTime + 4.0f && !flashing)
            {
                flashing = true;
                StartCoroutine(FlashText());
            }
        }
    }

    IEnumerator FlashText()
    {
        bool red = false;
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (red)
            {
                if (TimeManager.newHighTime)
                    highTime.color = new Color(1, 1, 0, 1);
                if (WaveNumber.newHighWave)
                    highWave.color = new Color(1, 1, 0, 1);
                if (ScoreManager.newHighScore)
                    highScore.color = new Color(1, 1, 0, 1);
            }
            else
            {
                if (TimeManager.newHighTime)
                    highTime.color = new Color(1, 0, 0, 1);
                if (WaveNumber.newHighWave)
                    highWave.color = new Color(1, 0, 0, 1);
                if (ScoreManager.newHighScore)
                    highScore.color = new Color(1, 0, 0, 1);
            }
            red = !red;
        }
    }
}
