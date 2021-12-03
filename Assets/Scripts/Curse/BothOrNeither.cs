using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BothOrNeither : MonoBehaviour
{
    public static List<Curse> curses;
    public static int rand;
    public static bool cursed;

    public static bool close;
    public static bool cursing;

    public static bool[] cursedUsed;

    // Start is called before the first frame update
    void Start()
    {
        curses = new List<Curse>();
        curses.Add(new Curse("You've gained all the curses!", "There are no more curses..."));
        curses.Add(new Curse("Your max health is tripled.\n\nCurrent max health: ", "You lose half of your damage reduction.\n\nCurrent damage reduction: "));
        curses.Add(new Curse("You gain 4 damage.\n\nCurrent damage: ", "Your bullet speed is halved.\n\nCurrent bullet speed: "));
        curses.Add(new Curse("You can now double jump.", "You lose 1 defence\n\nCurrent defence: "));
        curses.Add(new Curse("You gain 1 to each stat level.\n\nCurrent VIT/END/AGI/STR/DEX: ", "Touching grass will instead make you lose double of those stats. Jumping will give back your stats."));
        curses.Add(new Curse("Your fire rate is doubled\n\nCurrent fire rate: ", "Your damage is quartered.\n\nCurrent damage: "));
        curses.Add(new Curse("Your dash speed is quadrupled\n\nCurrent dash speed: ", "Your speed is quartered.\n\nCurrent speed: "));
        curses.Add(new Curse("You gain 2 defence.\n\nCurrent defence: ", "You lose 2 speed and 8 dash speed.\n\nCurrent speed: " + "\nCurrent dash speed: "));

        rand = Random.Range(1, curses.Count);
        cursedUsed = new bool[curses.Count + 1];
    }

    // Update is called once per frame
    void Update()
    {
        ActivateCurse();
        Debug.Log(rand);
    }

    void ActivateCurse()
    {
        if (cursed)
        {
            cursed = false;
            if (rand == 1)
            {
                PlayerController.maxHealth *= 3;
                PlayerController.damageReduction /= 2;
            }
            else if (rand == 2)
            {
                Shooter.bulletDamage += 4;
                Shooter.bulletSpeed /= 2;
            }
            else if (rand == 3)
            {
                PlayerController.maxJumps = 2;
                PlayerController.defence -= 1;
            }
            else if (rand == 4)
            {
                PlayerController.airBonus = true;
            }
            else if (rand == 5)
            {
                Shooter.fireRate *= 2;
                Shooter.bulletDamage /= 4;
            }
            else if (rand == 6)
            {
                PlayerController.dashSpeed *= 4;
                PlayerController.maxSpeed /= 4;
            }
            else if (rand == 7)
            {
                PlayerController.defence += 2;
                PlayerController.maxSpeed -= 2;
                PlayerController.dashSpeed -= 8;
            }
            else if (rand == 8)
            {

            }
            cursedUsed[rand] = true;
            Close();
        }
    }

    public static void RandomCurse()
    {
        curses[0] = (new Curse("You've gained all the curses!", "There are no more curses..."));
        curses[1] = (new Curse("Your max health is tripled.\n\nCurrent max health: " + PlayerController.maxHealth, "You take double damage."));
        curses[2] = (new Curse("You gain 4 damage.\n\nCurrent damage: " + Shooter.bulletDamage, "Your bullet speed is halved.\n\nCurrent bullet speed: " + Shooter.bulletSpeed));
        curses[3] = (new Curse("You can now double jump.", "You lose 1 defence\n\nCurrent defence: " + PlayerController.defence));
        curses[4] = (new Curse("You gain stats worth 1 VIT/END/AGI/STR/DEX while in the air.", "Touching grass will instead make you lose double of those stats. Jumping will give back your stats."));
        curses[5] = (new Curse("Your fire rate is doubled\n\nCurrent fire rate: " + Shooter.fireRate, "Your damage is quartered.\n\nCurrent damage: " + Shooter.bulletDamage));
        curses[6] = (new Curse("Your dash speed is quadrupled\n\nCurrent dash speed: " + PlayerController.dashSpeed, "Your speed is quartered.\n\nCurrent speed: " + PlayerController.maxSpeed));
        curses[7] = (new Curse("You gain 2 defence.\n\nCurrent defence: " + PlayerController.defence, "You lose 2 speed and 8 dash speed.\n\nCurrent speed: " + PlayerController.maxSpeed + "\nCurrent dash speed: " + PlayerController.dashSpeed));

        rand = Random.Range(1, curses.Count);
        bool curseExhasted = true;

        for (int i = 1; i < curses.Count; i++)
        {
            if (!cursedUsed[i]) { curseExhasted = false; break; }
        }

        if (!curseExhasted)
            while (cursedUsed[rand])
            {
                rand = Random.Range(1, curses.Count);
                Debug.Log(rand);
            }
        else rand = 0;
    }

    public static void Curse()
    {
        cursed = true;
    }

    public static void Close()
    {
        close = true;
        RandomCurse();
        cursing = false;
    }
}
