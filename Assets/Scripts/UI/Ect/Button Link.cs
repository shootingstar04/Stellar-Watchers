using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonLink : MonoBehaviour
{
    private ItemPopUp inventory;

    private void Awake()
    {
        inventory = GameObject.FindGameObjectWithTag(Define.PlayerTag).transform.Find("Inventory").GetComponent<ItemPopUp>();

        this.GetComponent<Button>().onClick.AddListener(inventory.off_PopUp);  
    }


    void Start()
    {
    }
}
