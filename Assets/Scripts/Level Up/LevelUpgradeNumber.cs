using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpgradeNumber : MonoBehaviour
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
        if (levelName.Equals("Vitality")) { text.text = LevelUp.vitUp.ToString(); }
        else if (levelName.Equals("Endurance")) { text.text = LevelUp.endUp.ToString(); }
        else if (levelName.Equals("Agility")) { text.text = LevelUp.agiUp.ToString(); }
        else if (levelName.Equals("Strength")) { text.text = LevelUp.strUp.ToString(); }
        else if (levelName.Equals("Dexterity")) { text.text = LevelUp.dexUp.ToString(); }
    }
}
