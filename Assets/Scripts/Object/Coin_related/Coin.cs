using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Pool;

public class Coin : MonoBehaviour
{
    private int gold = 1;


    private enum coins {oneCoin = 1, tenCoin, fifteenCoin }

    [SerializeField] private coins coin;

    private void Awake()
    {
        switch((int)coin)
        {
            case 1:
                gold = 1;
                break;
            case 2:
                gold = 10;
                break;
            case 3:
                gold = 15;
                break;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Define.PlayerTag))
        {
            Debug.Log(gold+"원 돈먹음");
            CollectInventory.Instance.get_coin(gold);
            DestroyCoin();
            
        }
    }
    
    public void DestroyCoin()
    {
        switch (gold)
        {
            case 1:
                CoinPool.ReturnObject(this, CoinPool.Instance.poolCoin1Queue);
                break;
            case 10:
                CoinPool.ReturnObject(this, CoinPool.Instance.poolCoinXQueue);
                break;
            case 15:
                CoinPool.ReturnObject(this, CoinPool.Instance.poolCoinXVQueue);
                break;
            default:
                Debug.Log("코인 삭제 에러");
                Destroy(this);
                break;
        }
    }
}
