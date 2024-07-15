using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
    

    [SerializeField] private GameObject box;
    [SerializeField] private GameObject coin;
    int MaxCoin;
    int MinCoin;
  
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
}
