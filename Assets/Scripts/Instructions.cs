using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Instructions : MonoBehaviour
{
    float startTime;
    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > startTime + 5)
        {
            sprite.color = Color.Lerp(new Color(1, 1, 1, 1), new Color(1, 1, 1, 0), (Time.time - startTime - 5) / 2);
        }
    }
}
