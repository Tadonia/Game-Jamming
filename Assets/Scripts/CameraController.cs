using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        float boundsX = Camera.main.orthographicSize * Camera.main.aspect;
        float boundsY = Camera.main.orthographicSize;

        Vector3 pos = transform.position;
        if (pos.x <= (-20 + boundsX) && player.transform.position.x >= (-20 + boundsX))
        {
            pos.x = -20 + boundsX;
        }
        else if (pos.x >= (20 - boundsX) && player.transform.position.x <= (20 - boundsX))
        {
            pos.x = 20 - boundsX;
        }
        if (pos.x >= (-20 + boundsX) && pos.x <= (20 - boundsX))
        {
            pos.x = player.transform.position.x;
        }
        pos.y = player.transform.position.y;
        pos.z = -10;
        transform.position = pos;
    }
}
