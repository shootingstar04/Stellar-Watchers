using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectInventory : MonoBehaviour
{
    public bool[] collectItems = new bool[10];
    private bool[] stars = new bool[15];
    private bool[] keys = new bool[2];

    private GameObject collection;
    private GameObject constellation;

    // Start is called before the first frame update
    void Awake()
    {
        collection = GameObject.Find("Collection");

        collection.GetComponent<CollectPopUp>().acquired = collectItems;

        for (int i = 0; i < collectItems.Length; i++) {
            collectItems[i] = false;
        }

        for (int i = 0; i < stars.Length; i++)
        {
            stars[i] = false;
        }

        for (int i = 0; i < keys.Length; i++)
        {
            keys[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Item>() != null)
        {
            Debug.Log(1111);

            Item item = collision.gameObject.GetComponent<Item>();

            switch (item.itemType) { 
                case 0:
                    if(item.itemNum<collectItems.Length)
                        collectItems[item.itemNum] = true;
                    break;
                case 1:
                    if (item.itemNum < stars.Length)
                        stars[item.itemNum] = true;
                    break;
                case 2:
                    if (item.itemNum < keys.Length)
                        keys[item.itemNum] = true;
                    break;
            }

            Destroy(collision.gameObject);
        }
    }
}
