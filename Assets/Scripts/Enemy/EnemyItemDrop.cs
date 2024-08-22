using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyItemDrop : MonoBehaviour
{
    public void DropCoins(int coinYield)
    {
        int coins = 0;
        Queue<int> CoinNumQueue = new Queue<int>();

        if (coinYield > 10)
        {
            CoinNumQueue.Enqueue(coinYield % 10);
            CoinNumQueue.Enqueue(coinYield / 10);
        }
        else
        {
            CoinNumQueue.Enqueue(coinYield);
        }

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
    }

    private (Queue<Coin>, GameObject) WhatCoinCurrently(int coin)
    {
        switch (coin)
        {
            case 1:
                return (CoinPool.Instance.poolCoin1Queue, CoinPool.Instance.Coin1);
            case 2:
                return (CoinPool.Instance.poolCoinXQueue, CoinPool.Instance.CoinX);
            case 3:
                return (CoinPool.Instance.poolCoinXVQueue, CoinPool.Instance.CoinXV);

        }
        return (null, null);

    }
}
