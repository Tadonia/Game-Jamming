using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public static float timer;
    Text text;
    bool gameOver;

    public static bool newHighTime;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0;
        text = GetComponent<Text>();
        newHighTime = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelUp.leveling && !BothOrNeither.cursing && !GameOverManager.gameOver && WaveManager.gameStart)
            timer += Time.deltaTime;
        string fraction = ((timer - (int)timer) * 100).ToString("00");
        string seconds = ((int)timer % 60).ToString("00");
        //string minutes = (((int)timer - int.Parse(seconds)) / 60).ToString();
        string minutes = ((int)(timer / 60)).ToString("00");
        text.text = minutes + ":" + seconds + ":" + fraction;
        if (GameOverManager.gameOver && !gameOver)
        {
            gameOver = true;
            if (PlayerPrefs.GetFloat("Time", 0) < timer)
            {
                PlayerPrefs.SetFloat("Time", timer);
                newHighTime = true;
            }
}
    }
}
