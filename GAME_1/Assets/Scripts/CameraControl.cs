using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Transform player;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player_1").transform;
    }
    private void Update()
    {
        Vector3 temp = transform.position;
        temp.x = player.position.x;
        temp.y = player.position.y;
        temp.z = -10f;
        transform.position = temp;
    }
}
