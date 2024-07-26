using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextPopUp : MonoBehaviour
{
    public static TextPopUp instance;

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
        popUpScreen = this.transform.Find("Text PopUp").gameObject;
        off_PopUp();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void show_PopUp(string text)
    {
        off_PopUp();
        popUpScreen.SetActive(true);
        UIManager.instance.pause();
        UIManager.instance.show_popup();
        popUpScreen.transform.Find("Text").GetComponent<Text>().text = text;
        popUpScreen.transform.Find("confirm").GetComponent<Button>().Select();
    }
    public void off_PopUp()
    {
        popUpScreen.SetActive(false);
        UIManager.instance.start();
        UIManager.instance.off_PopUp();
    }
}
