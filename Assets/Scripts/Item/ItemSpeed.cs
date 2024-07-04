using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpeed : MonoBehaviour
{
    [SerializeField] private GameObject itemSpeedManager;
    private ItemSpeedManager other;
    private GameObject player;
    private PlayerMove pMove;

    private void Awake()
    {
        other = itemSpeedManager.GetComponent<ItemSpeedManager>();
    }


    private void Update()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
            
            pMove = player.GetComponent<PlayerMove>();
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            other.isTriggered = true;
            pMove.moveSpeed = pMove.moveSpeed * 1.6f;
            Destroy(this.gameObject);
        }
    }
}
