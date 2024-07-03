using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    private GameObject Player;


    private void Start()
    {
        this.Player = GameObject.Find("Player");
        target = this.Player.GetComponent<Transform>();
    }

    private void FixedUpdate()
    {
        transform.position = target.position+Vector3.back;
    }
}
