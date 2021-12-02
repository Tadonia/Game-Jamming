using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsCurrentNumber : MonoBehaviour
{
    Text text;
    Shooter shooter;
    string statName;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        shooter = GameObject.FindGameObjectWithTag("Player").GetComponent<Shooter>();
        statName = transform.parent.name;
    }

    // Update is called once per frame
    void Update()
    {
        if (statName.Equals("Health")) { text.text = decimal.Round((decimal)PlayerController.maxHealth, 1).ToString(); }
        else if (statName.Equals("Defence")) { text.text = decimal.Round((decimal)PlayerController.defence, 1).ToString(); }
        else if (statName.Equals("Speed")) { text.text = decimal.Round((decimal)PlayerController.maxSpeed, 1).ToString(); }
        else if (statName.Equals("Dash Speed")) { text.text = decimal.Round((decimal)PlayerController.dashSpeed, 1).ToString(); }
        else if (statName.Equals("Damage")) { text.text = decimal.Round((decimal)Shooter.bulletDamage, 1).ToString(); }
        else if (statName.Equals("Fire Rate")) { text.text = decimal.Round((decimal)Shooter.fireRate, 1).ToString(); }
        else if (statName.Equals("Bullet Speed")) { text.text = decimal.Round((decimal)Shooter.bulletSpeed, 1).ToString(); }
        else if (statName.Equals("Bullet Size")) { text.text = decimal.Round((decimal)Shooter.bulletSize, 1).ToString(); }
    }
}
