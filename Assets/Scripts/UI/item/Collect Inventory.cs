using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectInventory : MonoBehaviour
{
    private bool[] keys = new bool[2];

    private GameObject collection;

    public static CollectInventory Instance;

    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null)
            Instance = this;

        collection = GameObject.Find("Collection");

        for (int i = 0; i < ItemData.Instance.Acquired.Count; i++) {
            ItemData.Instance.Acquired[i] = false;
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

    public void get_coin(int goldValue) 
    {
        ItemData.Instance.modify_gold(goldValue);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        GameObject other = collision.gameObject;
        if (other.GetComponent<CollectItem>() != null && !other.GetComponent<CollectItem>().isActived)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                CollectItem item = other.GetComponent<CollectItem>();

                switch (((int)item.itemType)) {
                    case 0:
                        if (item.itemNum < ItemData.Instance.Acquired.Count)
                            ItemData.Instance.Acquired[item.itemNum] = true;
                        break;
                    case 1:
                        if (item.itemNum < keys.Length)
                            keys[item.itemNum] = true;
                        break;
                }

                this.GetComponent<ItemPopUp>().show_PopUP((int) item.itemType, item.itemNum);

                item.StartCoroutine(item.destroy_item());
            }
        }
    }
}
