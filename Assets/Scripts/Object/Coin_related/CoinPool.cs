using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPool : MonoBehaviour
{
    public static CoinPool Instance;

    [SerializeField] public GameObject Coin1;
    [SerializeField] public GameObject CoinX;
    [SerializeField] public GameObject CoinXV;


    public Queue<Coin> poolCoin1Queue = new Queue<Coin>();
    public Queue<Coin> poolCoinXQueue = new Queue<Coin>();
    public Queue<Coin> poolCoinXVQueue = new Queue<Coin>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Initialize(10, poolCoin1Queue, Coin1);
        Initialize(10, poolCoinXQueue, CoinX);
        Initialize(10, poolCoinXVQueue, CoinXV);
    }

    private void Initialize(int initCount, Queue<Coin> queue, GameObject obj)
    {
        for (int i = 0; i < initCount; i++)
        {
            queue.Enqueue(CreateNewObject(obj));
        }
    }

    private Coin CreateNewObject(GameObject obj)
    {
        var newObj = Instantiate(obj).GetComponent<Coin>();
        newObj.gameObject.SetActive(false);
        newObj.transform.SetParent(transform);
        return newObj;
    }

    public static Coin GetObject(Queue<Coin> queue, GameObject OBJ)
    {
        if (queue.Count > 0)
        {
            var obj = queue.Dequeue();
            obj.transform.SetParent(null);
            obj.gameObject.SetActive(true);
            return obj;
        }
        else
        {
            var newObj = Instance.CreateNewObject(OBJ);
            newObj.gameObject.SetActive(true);
            newObj.transform.SetParent(null);
            return newObj;
        }
    }
    public static void ReturnObject(Coin obj, Queue<Coin> queue)
    {
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(Instance.transform);
        queue.Enqueue(obj);
    }
}
