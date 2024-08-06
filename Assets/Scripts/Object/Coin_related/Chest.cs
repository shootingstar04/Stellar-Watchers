using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    //상자 정보(대, 중, 소)
    //코인 정보(프리팹)
    //파괴 정보

    //코인 1 5 10
    // S - 10코인 x
    // B - 10코인 3
    // C - 10코인 5

    public enum coinKind { small = 1, big, chest }

    [SerializeField] public coinKind _coinKind;
    private int hitCount = 0;

    Queue<int> CoinNumQueue = new Queue<int>();

    private Sprite sprite;

    int coins = 0;

    [SerializeField] private Collider2D col;
    [SerializeField] private GameObject popup;

    private void Awake()
    {
        switch ((int)_coinKind)
        {
            case 1:
                Destroy(col);
                Destroy(popup);
                CoinNumQueue.Enqueue(6);
                CoinNumQueue.Enqueue(0);
                CoinNumQueue.Enqueue(0);    //1사이클
                CoinNumQueue.Enqueue(7);
                CoinNumQueue.Enqueue(0);
                CoinNumQueue.Enqueue(0);    //2사이클
                CoinNumQueue.Enqueue(7);
                CoinNumQueue.Enqueue(1);
                CoinNumQueue.Enqueue(0);    //3사이클
                hitCount = 3;
                break;
            case 2:
                Destroy(col);
                Destroy(popup);
                CoinNumQueue.Enqueue(3);
                CoinNumQueue.Enqueue(1);
                CoinNumQueue.Enqueue(0);    //1사이클
                CoinNumQueue.Enqueue(3);
                CoinNumQueue.Enqueue(0);
                CoinNumQueue.Enqueue(1);    //2사이클
                CoinNumQueue.Enqueue(4);
                CoinNumQueue.Enqueue(0);
                CoinNumQueue.Enqueue(1);    //3사이클
                hitCount = 3;
                break;
            case 3:
                CoinNumQueue.Enqueue(10);
                CoinNumQueue.Enqueue(3);
                CoinNumQueue.Enqueue(4);    //1사이클 
                hitCount = 1;
                break;
            default:
                CoinNumQueue.Enqueue(0);
                break;

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
        coins = 0;
        hitCount--;
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

            if (hitCount <= 0)
            {
                GetComponent<GetSOindex>().returnBool();
                Destroy(this.gameObject);
            }
            else if (coins >= 3)
            {
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((int)_coinKind != 3)
        {
            return;
        }

        if (collision.CompareTag(Define.PlayerTag))
        {
            popup.SetActive(true);
        }

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if ((int)_coinKind != 3)
        {
            return;
        }

        if (other.CompareTag(Define.PlayerTag))
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                coins = 0;
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


                    GetComponent<GetSOindex>().returnBool();
                    Destroy(this.gameObject);

                }
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        if ((int)_coinKind != 3)
        {
            return;
        }

        if (collision.CompareTag(Define.PlayerTag))
        {
            popup.SetActive(false);
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
