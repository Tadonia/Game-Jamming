using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUpgradeNumber : MonoBehaviour
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
        if (levelName.Equals("Health")) { text.text = (LevelUp.vitUp * 1.5f).ToString(); }
        else if (levelName.Equals("Defence")) { text.text = (LevelUp.endUp * 0.2f).ToString(); }
        else if (levelName.Equals("Speed")) { text.text = (LevelUp.agiUp * 0.2f).ToString(); }
        else if (levelName.Equals("Dash Speed")) { text.text = (LevelUp.agiUp * 0.3f).ToString(); }
        else if (levelName.Equals("Damage")) { text.text = (LevelUp.strUp * 0.3f).ToString(); }
        else if (levelName.Equals("Fire Rate")) { text.text = (LevelUp.dexUp * 0.2f).ToString(); }
        else if (levelName.Equals("Bullet Speed")) { text.text = (LevelUp.dexUp * 1.5f).ToString(); }
        else if (levelName.Equals("Bullet Size")) { text.text = (LevelUp.strUp * 0.1f).ToString(); }

        text.text = float.Parse(text.text).ToString("+(0.00)");
        if (text.text.Equals("+(0.00)")) { text.text = ""; }
    }
}
