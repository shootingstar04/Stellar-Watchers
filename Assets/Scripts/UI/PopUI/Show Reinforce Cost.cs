using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowReinforceCost : MonoBehaviour
{
    private int cost = 0;
    [TextArea]
    public string generalText = "���⸦ ��ȭ �Ͻðڽ��ϱ�?\n��� : x";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        set_cost();
    }

    public void set_cost()
    {
        switch (ProgressData.Instance.reinforcementCount)
        {
            case 0:
                cost = 200;
                break;
            case 1:
                cost = 300;
                break;
            case 2:
                cost = 500;
                break;
            default:
                cost = 0;
                break;
        }
        Debug.Log(cost);
        if (cost != 0)
        {
            string changeText = generalText.Replace("x", cost.ToString());
            this.GetComponent<Text>().text = changeText;
        }
        else
        {
            this.GetComponent<Text>().text = "�� �̻� ���⸦ ��ȭ �� �� �����ϴ�.";
        }
    }
}
