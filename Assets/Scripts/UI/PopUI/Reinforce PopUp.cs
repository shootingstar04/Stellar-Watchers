using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReinforcePopUp : MonoBehaviour
{
    public static ReinforcePopUp instance;

    private GameObject popUpScreen;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        popUpScreen = this.transform.Find("Reinforce PopUp").gameObject;
        off_PopUp();
    }

    // Update is called once per frame
    void Update()
    {
    }
    public void reinforce_weapon()
    {
        int cost;
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
        if (cost < ItemData.Instance.CurrentGold)
        {
            ProgressData.Instance.reinforcementCount += 1;
            ItemData.Instance.modify_gold(-cost);
        }
        else 
        {
            TextPopUp.instance.show_PopUp("돈이 부족합니다.");
        }
    }

    public void show_PopUp()
    {
        popUpScreen.SetActive(true);
        UIManager.instance.pause();
        UIManager.instance.show_popup();
        popUpScreen.transform.Find("confirm").GetComponent<Button>().Select();
    }
    public void off_PopUp()
    {
        popUpScreen.SetActive(false);
        UIManager.instance.start();
        UIManager.instance.off_PopUp();
    }
}
