using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveNumber : MonoBehaviour
{
    public WaveManager wave;
    Text text;
    bool gameOver;
    public static bool newHighWave;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        newHighWave = false;
    }

    // Update is called once per frame
    void Update()
    {
        text.text = wave.waveNum.ToString();
        if (GameOverManager.gameOver && !gameOver)
        {
            gameOver = true;
            if (PlayerPrefs.GetInt("Wave", 0) < wave.waveNum)
            {
                PlayerPrefs.SetInt("Wave", wave.waveNum);
                newHighWave = true;
            }
        }
    }
}
