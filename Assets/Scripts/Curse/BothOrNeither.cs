using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BothOrNeither : MonoBehaviour
{
    public List<Curse> curses;
    public int rand;
    public static bool cursed;
    public static bool close;

    bool[] cursedUsed;

    // Start is called before the first frame update
    void Start()
    {
        curses = new List<Curse>();
        curses.Add(new Curse("You've gained all the curses!", "There are no more curses..."));
        curses.Add(new Curse("Your max health is tripled.\n\nCurrent max health: ", "You lose half of your damage reduction.\n\nCurrent damage reduction: "));
        curses.Add(new Curse("You gain 4 damage.\n\nCurrent damage: ", "Your bullet speed is halved.\n\nCurrent bullet speed: "));
        curses.Add(new Curse("You can now double jump.", "You lose 1 defence\n\nCurrent defence: "));
        curses.Add(new Curse("You gain 1 to each stat level.\n\nCurrent VIT/END/AGI/STR/DEX: ", "Touching grass will instead make you lose those stats. Jumping will give back your stats."));
        curses.Add(new Curse("Your fire rate is doubled\n\nCurrent fire rate: ", "Your damage is quartered.\n\nCurrent damage: "));

        rand = Random.Range(1, curses.Count);
        cursedUsed = new bool[curses.Count + 1];
    }

    // Update is called once per frame
    void Update()
    {
        ActivateCurse();
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
                PlayerController.defence -= 1;
            }
            else if (rand == 4)
            {
                
            }
            else if (rand == 5)
            {
                Shooter.fireRate *= 2;
                Shooter.bulletDamage /= 4;
            }
            else if (rand == 6)
            {
                
            }
            cursedUsed[rand] = true;
            RandomCurse();
            Close();
        }
    }

    void RandomCurse()
    {
        bool curseExhasted = true;

        for (int i = 1; i < curses.Count; i++)
        {
            if (!cursedUsed[i]) { curseExhasted = false; break; }
        }

        if (!curseExhasted)
            while (cursedUsed[rand])
            {
                rand = Random.Range(1, curses.Count);
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
    }
}
