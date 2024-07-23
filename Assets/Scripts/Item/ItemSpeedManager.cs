using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpeedManager : MonoBehaviour
{
    [SerializeField] private GameObject item;
    private GameObject player;
    private PlayerMove pMove;
    private ItemSpeed thing;
    internal bool isTriggered = false;
    private float timer;
    private static float buffTime = 10f;

    private void Awake()
    {
        thing = item.GetComponent<ItemSpeed>();
    }

    private void Update()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");

            pMove = player.GetComponent<PlayerMove>();
        }


        if (isTriggered)
        {
            timer += Time.deltaTime;
            if (timer >= buffTime)
            {
                pMove.moveSpeed = pMove.moveSpeed * 0.625f;
                isTriggered = false;
                Destroy(this.gameObject);
            }
        }
    }



}
