using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    //���� ����(��, ��, ��)
    //���� ����(������)
    //�ı� ����

    [SerializeField] private GameObject box;
    [SerializeField] private GameObject coin;
    int MaxCoin;
    int MinCoin;

    static public Chest chest;

    private void Awake()
    {
        chest = this;
    }


    private void Start()
    {
        string boxSize = box.name;
        switch(boxSize)
        {
            case "SmallRock":
                {
                    MaxCoin = 51;
                    MinCoin = 30;
                }
                return;
            case "BigRock":
                {
                    MaxCoin = 81;
                    MinCoin = 65;
                }
                return;
            case "CoinChest":
                {
                    MaxCoin = 100;
                    MinCoin = 100;
                }
                return;
            default:
                {
                    MaxCoin = 0;
                    MinCoin = 0;
                }
                return;
        }
    }

    public void Distroyed()
    {
        int LootCoin = Random.Range(MinCoin, MaxCoin);
        for(int i  = LootCoin;  i >=0; LootCoin --)
        {
            GameObject myInstance = Instantiate(coin);
            var direction = Random.Range(-1000, 1000);
            var force = 10f;
            myInstance.GetComponent<Rigidbody2D>().AddForce(new Vector2(direction , force));
        }
        Destroy(this);
    }
}
