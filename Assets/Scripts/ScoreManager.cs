using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static int score;
    public WaveManager wave;
    Text text;
    bool gameOver;

    public static int initialScore;
    public static int timeBonus;
    public static int waveBonus;
    public static int curseBonus;
    public static int finalScore;

    public static bool newHighScore;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        text = GetComponent<Text>();
        initialScore = 0;
        timeBonus = 0;
        waveBonus = 0;
        curseBonus = 0;
        finalScore = 0;
        newHighScore = false;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = score.ToString("000000");
        if (GameOverManager.gameOver && !gameOver)
        {
            gameOver = true;
            initialScore = score;
            timeBonus = (int)TimeManager.timer * 10;
            waveBonus = wave.waveNum * 100;
            curseBonus = BothOrNeither.numOfCursesUsed * 500;
            finalScore = initialScore + timeBonus + waveBonus + curseBonus;
            if (PlayerPrefs.GetInt("Score", 0) < finalScore)
            {
                PlayerPrefs.SetInt("Score", finalScore);
                newHighScore = true;
            }
        }
    }
}
