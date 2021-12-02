using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelCurrentNumber : MonoBehaviour
{
    Text text;
    string levelName;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        levelName = transform.parent.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (levelName.Equals("Points")) { text.text = LevelUp.points.ToString(); }
        else if (levelName.Equals("Vitality")) { text.text = LevelUp.vit.ToString(); }
        else if (levelName.Equals("Endurance")) { text.text = LevelUp.end.ToString(); }
        else if (levelName.Equals("Agility")) { text.text = LevelUp.agi.ToString(); }
        else if (levelName.Equals("Strength")) { text.text = LevelUp.str.ToString(); }
        else if (levelName.Equals("Dexterity")) { text.text = LevelUp.dex.ToString(); }
    }
}
