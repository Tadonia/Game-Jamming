using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    Transform player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        float boundsX = Camera.main.orthographicSize * Camera.main.aspect;
        float left = -20 + boundsX;
        float right = 20 - boundsX;

        Vector3 pos = player.position;
        if (pos.x > right) { pos.x = right; }
        if (pos.x < left) { pos.x = left; }

        pos.z = -10;
        transform.position = pos;
    }
}
