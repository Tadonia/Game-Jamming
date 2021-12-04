using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadStartScene : MonoBehaviour
{
    public SpriteRenderer background;
    public Image startButton;
    public Text startText;
    public Image highScoreButton;
    public Text highScoreText;
    static bool startClick;
    static float startTime;

    // Start is called before the first frame update
    void Start()
    {
        startClick = false;
        startTime = 0;
        background.color = new Color(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (startClick)
        {
            background.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 1), (Time.time - startTime) / 0.5f);
            startButton.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 1), (Time.time - startTime) / 0.5f);
            highScoreButton.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 1), (Time.time - startTime) / 0.5f);
            startText.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 1), (Time.time - startTime) / 0.5f);
            highScoreText.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 1), (Time.time - startTime) / 0.5f);
            if (Time.time > startTime + 0.5f)
            {
                SceneManager.LoadScene(1);
            }
        }
    }

    public static void LoadScene()
    {
        if (!startClick)
        {
            startClick = true;
            startTime = Time.time;
        }
    }
}
