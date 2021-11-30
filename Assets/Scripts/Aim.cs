using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    Transform player;
    Transform arm;

    // Start is called before the first frame update
    void Start()
    {
        player = transform.parent.transform;
        arm = transform.parent.GetComponentInChildren<Shooter>().GetArm();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 aimPos = (mousePos - player.position).normalized;
        aimPos = new Vector3(aimPos.x, aimPos.y, 0);

        Vector3 aimStartPos = arm.GetChild(0).position;

        RaycastHit2D hit = Physics2D.Raycast(aimStartPos, aimPos, 40, ~576);
        //Debug.DrawLine(player.position, hit.point, Color.red);
        //Debug.DrawRay(player.position, aimPos * 10, Color.yellow);
        if (hit.point != Vector2.zero)
            transform.position = hit.point;
        else
            transform.position = new Vector2(0, -10);
    }
}
