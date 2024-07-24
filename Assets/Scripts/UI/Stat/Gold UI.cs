using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldUI : MonoBehaviour
{
    private Text currentGold;
    // Start is called before the first frame update
    void Start()
    {
        currentGold = this.transform.Find("Current Gold").GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        setGold();
    }

    private void setGold()
    {
        if (int.Parse(currentGold.text) != ItemData.Instance.CurrentGold) 
        {
            currentGold.text = ItemData.Instance.CurrentGold.ToString();
        }
    }
}
