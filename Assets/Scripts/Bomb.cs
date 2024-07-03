using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private Rigidbody2D rigid;

    private GameObject player;



    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if(player== null)
        {
            player = GameObject.FindGameObjectWithTag(Define.PlayerTag);
        }


        if (Input.GetKeyDown(KeyCode.X))
        {
            //PlayerMove.Move.moveInput
        }
    }


    void Attacked()
    {
        player.GetComponentInChildren<GameObject>();
    }


}
