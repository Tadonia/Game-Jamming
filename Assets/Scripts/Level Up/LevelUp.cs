using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUp : MonoBehaviour
{
    public static int points;

    public static int vit;
    public static int end;
    public static int agi;
    public static int str;
    public static int dex;

    public static int vitUp;
    public static int endUp;
    public static int agiUp;
    public static int strUp;
    public static int dexUp;

    public static bool close;
    public static bool leveling;

    // Start is called before the first frame update
    void Start()
    {
        points = 0;
        vit = 10;
        end = 10;
        agi = 10;
        str = 10;
        dex = 10;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PointsUp()
    {
        points += 5;
        leveling = true;
    }

    public static void VitUp()
    {
        if (points > 0 && Input.GetKeyDown(KeyCode.Mouse0))
        {
            vitUp += 1;
            points -= 1;
        }
    }

    public static void VitDown()
    {
        if (vitUp > 0 && Input.GetKeyDown(KeyCode.Mouse0))
        {
            vitUp -= 1;
            points += 1;
        }
    }

    public static void EndUp()
    {
        if (points > 0 && Input.GetKeyDown(KeyCode.Mouse0))
        {
            endUp += 1;
            points -= 1;
        }
    }

    public static void EndDown()
    {
        if (endUp > 0 && Input.GetKeyDown(KeyCode.Mouse0))
        {
            endUp -= 1;
            points += 1;
        }
    }

    public static void AgiUp()
    {
        if (points > 0 && Input.GetKeyDown(KeyCode.Mouse0))
        {
            agiUp += 1;
            points -= 1;
        }
    }

    public static void AgiDown()
    {
        if (agiUp > 0 && Input.GetKeyDown(KeyCode.Mouse0))
        {
            agiUp -= 1;
            points += 1;
        }
    }

    public static void StrUp()
    {
        if (points > 0 && Input.GetKeyDown(KeyCode.Mouse0))
        {
            strUp += 1;
            points -= 1;
        }
    }

    public static void StrDown()
    {
        if (strUp > 0 && Input.GetKeyDown(KeyCode.Mouse0))
        {
            strUp -= 1;
            points += 1;
        }
    }

    public static void DexUp()
    {
        if (points > 0 && Input.GetKeyDown(KeyCode.Mouse0))
        {
            dexUp += 1;
            points -= 1;
        }
    }

    public static void DexDown()
    {
        if (dexUp > 0 && Input.GetKeyDown(KeyCode.Mouse0))
        {
            dexUp -= 1;
            points += 1;
        }
    }

    public static void UpgradeComplete()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && points == 0)
        {
            vit += vitUp;
            end += endUp;
            agi += agiUp;
            str += strUp;
            dex += dexUp;

            PlayerController.maxHealth += vitUp * 1.5f + PlayerController.healthBonus;
            PlayerController.defence += endUp * 0.2f;
            PlayerController.maxSpeed += agiUp * 0.3f;
            PlayerController.dashSpeed += agiUp * 0.4f;
            Shooter.bulletDamage += strUp * 0.3f;
            Shooter.fireRate += dexUp * 0.2f;
            Shooter.bulletSpeed += dexUp * 1.5f;
            Shooter.bulletSize += strUp * 0.1f;

            vitUp = 0;
            endUp = 0;
            agiUp = 0;
            strUp = 0;
            dexUp = 0;

            close = true;
            leveling = false;
        }
    }
}
