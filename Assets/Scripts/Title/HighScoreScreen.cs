using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreScreen : MonoBehaviour
{
    public static bool show;
    public static float startTime;
    RectTransform rect;
    public RectTransform thing;
    public Image background;

    // Start is called before the first frame update
    void Start()
    {
        show = false;
        startTime = 0;
        rect = GetComponent<RectTransform>();
        rect.localScale = new Vector3(0, 0, 1);
        background.color = new Color(0, 0, 0, 0);
        thing.anchoredPosition = new Vector2(1920, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (show)
        {
            rect.localScale = Vector3.Lerp(new Vector3(0, 0, 1), new Vector3(6, 6, 1), (Time.time - startTime) / 0.1f);
            background.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(0, 0, 0, 0.5f), (Time.time - startTime) / 0.1f);
            thing.anchoredPosition = new Vector2(0, 0);
        }
        else
        {
            rect.localScale = new Vector3(0, 0, 1);
            background.color = new Color(0, 0, 0, 0);
            thing.anchoredPosition = new Vector2(1920, 0);
        }
    }

    public static void ShowHighScore()
    {
        startTime = Time.time;
        show = true;
    }

    public static void HideHighScore()
    {
        show = false;
    }
}
