using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Curse
{
    public string Boon { get; private set; }
    public string Bane{ get; private set; }

    public Curse(string boon, string bane)
    {
        this.Boon = boon;
        this.Bane = bane;
    }
}
