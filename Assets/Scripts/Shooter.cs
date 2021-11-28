using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    LineRenderer line;
    Vector3 aimPos;

    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, transform.position);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        aimPos = (mousePos - transform.position).normalized;
        aimPos = new Vector3(aimPos.x, aimPos.y, 0);
        //Debug.Log(aimPos);
        line.SetPosition(1, aimPos * 1000);
    }
}
