using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreNumber : MonoBehaviour
{
    Text text;
    string scoreName;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        scoreName = transform.parent.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreName.Equals("Score")) text.text = ScoreManager.initialScore.ToString();
        else if (scoreName.Equals("Time Bonus")) text.text = (int)TimeManager.timer + "x10";
        else if (scoreName.Equals("Wave Bonus")) text.text = (ScoreManager.waveBonus/100) + "x100";
        else if (scoreName.Equals("Curse Bonus")) text.text = ScoreManager.curseBonus + "x500";
        else if (scoreName.Equals("Final Score")) text.text = ScoreManager.finalScore.ToString();
    }
}
