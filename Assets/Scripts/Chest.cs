using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;
using static Unity.VisualScripting.Member;

public class Chest : MonoBehaviour
{
    //상자 정보(대, 중, 소)
    //코인 정보(프리팹)
    //파괴 정보

    //코인 1 5 10
    // S - 10코인 x
    // B - 10코인 3
    // C - 10코인 5


    [SerializeField] private GameObject box;
    Queue<int> CoinNumQueue = new Queue<int>();

    int coins = 0;

    private void Start()
    {
        string boxSize = box.name;
        switch (boxSize)
        {
            case "SmallRock":
                {
                    CoinNumQueue.Enqueue(10);
                }
                return;
            case "BigRock":
                {
                    CoinNumQueue.Enqueue(0);
                    CoinNumQueue.Enqueue(8);
                }
                return;
            case "CoinChest":
                {
                    CoinNumQueue.Enqueue(0);
                    CoinNumQueue.Enqueue(4);
                    CoinNumQueue.Enqueue(5);
                }
                return;
            default:
                {
                }
                return;
        }
    }
    /*
    public void Distroyed()
    {
        int LootCoin = Random.Range(MinCoin, MaxCoin);
        for(int i = 0;  i < LootCoin; i ++)
        {
            Vector3 axis = Vector3.back;
            float angle = Random.Range(-90f, 90f);

            GameObject myInstance = Instantiate(coin, new Vector3(this.transform.position.x,this.transform.position.y,Vector3.zero.z), Quaternion.AngleAxis(angle,axis));

            //float rotation = Random.Range(-180f, 180f);
            //myInstance.transform.Rotate(0f,0f, rotation);

            var Xdirection = Random.Range(-1f, 1f);
            var Ydirection = 1f;
            Vector2 dir = new Vector2(Xdirection, Ydirection).normalized;
            float force = Random.Range(100f, 300f);
            
            myInstance.GetComponent<Rigidbody2D>().AddForce(dir * force);
        }
        Destroy(this.gameObject);
    }
    */

    public void Distroyed()
    {
        while (CoinNumQueue.Count > 0)
        {
            coins += 1;
            var count = CoinNumQueue.Dequeue();

            for (int i = 0; i < count; i++)
            {
                var (q, obj) = WhatCoinCurrently(coins);
                var Coin = CoinPool.GetObject(q, obj);

                Coin.transform.position = this.transform.position;

                float rotation = Random.Range(-90f, 90f);
                Coin.transform.Rotate(0f, 0f, rotation);

                var Xdirection = Random.Range(-1f, 1f);
                var Ydirection = 1f;
                Vector2 dir = new Vector2(Xdirection, Ydirection).normalized;
                float force = Random.Range(100f, 300f);
                Coin.GetComponent<Rigidbody2D>().AddForce(dir * force);
            }

        }
        Destroy(this.gameObject);
    }

    private (Queue<Coin>, GameObject) WhatCoinCurrently(int coin)
    {
        switch (coin)
        {
            case 1:
                return (CoinPool.Instance.poolCoin1Queue, CoinPool.Instance.Coin1);
            case 2:
                return (CoinPool.Instance.poolCoinVQueue, CoinPool.Instance.CoinV);
            case 3:
                return (CoinPool.Instance.poolCoinXQueue, CoinPool.Instance.CoinX);

        }
        return (null, null);

    }
}
