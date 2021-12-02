using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boon : MonoBehaviour
{
    Text text;
    BothOrNeither curse;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        curse = GetComponentInParent<BothOrNeither>();
    }

    // Update is called once per frame
    void Update()
    {
        text.text = curse.curses[curse.rand].Boon;
    }
}
