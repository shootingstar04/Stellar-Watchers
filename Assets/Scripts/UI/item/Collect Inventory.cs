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

                switch ((item.itemType)) {

                    case CollectItem.type.collect:
                        if (item.itemNum < ItemData.Instance.Acquired.Count)
                            ItemData.Instance.Acquired[item.itemNum] = true;
                        break;

                    case CollectItem.type.progress:
                        if (item.itemNum < ProgressData.Instance.Acquired.Count)
                            ProgressData.Instance.Acquired[item.itemNum] = true;
                        break;

                    case CollectItem.type.skill:
                        if (item.itemNum < SkillData.Instance.Acquired.Count)
                            SkillData.Instance.Acquired[item.itemNum] = true;
                        break;

                    case CollectItem.type.reinforcementStone:
                        if (item.itemNum < ProgressData.Instance.Acquired.Count)
                        {
                            ProgressData.Instance.Acquired[3] = true;
                            ProgressData.Instance.stoneCount += 1;
                        }
                        break;

                    case CollectItem.type.point:
                        ProgressData.Instance.pointCount += 1;
                        break;

                }

                if (item.showPopUp) {
                    this.GetComponent<ItemPopUp>().show_PopUP(item);
                }

                item.StartCoroutine(item.destroy_item());
            }
        }
    }
}
