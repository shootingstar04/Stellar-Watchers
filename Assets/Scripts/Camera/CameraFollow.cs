using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    private Transform target;
    private GameObject Player;
    private Rigidbody2D rigid;

    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private float smoothTime = 0.25f;
    private Vector3 velocity = Vector3.zero;
   
    private void Update()
    {
        if(Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
            target = this.Player.GetComponent<Transform>();
            rigid = this.Player.GetComponent<Rigidbody2D>();
        }

        if(rigid.velocity.y != 0f)
        {
            offset = new Vector3(0f, 0f, -10f);
        }
        else
        {
            offset = new Vector3(0f, 2f, -10f);  
        }

        Vector3 targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
