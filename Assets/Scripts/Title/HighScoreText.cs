using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreText : MonoBehaviour
{
    Text text;
    string parName;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        parName = transform.parent.name;
    }

    // Update is called once per frame
    void Update()
    {
        float timer = PlayerPrefs.GetFloat("Time", 0);

        string fraction = ((timer - (int)timer) * 100).ToString("00");
        string seconds = ((int)timer % 60).ToString("00");
        string minutes = ((int)(timer / 60)).ToString("00");

        if (parName.Equals("Score")) { text.text = PlayerPrefs.GetInt("Score", 0).ToString("000000"); }
        else if (parName.Equals("Time")) { text.text = minutes + ":" + seconds + ":" + fraction; }
        else if (parName.Equals("Wave")) { text.text = PlayerPrefs.GetInt("Wave", 0).ToString(); }
    }
}
