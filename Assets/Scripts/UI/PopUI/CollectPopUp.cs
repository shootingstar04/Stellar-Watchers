using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectPopUp : MonoBehaviour
{
    public List<string> storys = new List<string>();
    public List<string> toolTips = new List<string>();
    public List<string> names = new List<string>();
    public List<Sprite> images = new List<Sprite>();

    private GameObject tooltipScreen;
    private GameObject buttons;
    private UIManager UIManager;

    private bool isShowing = false;

    // Start is called before the first frame update
    void Awake()
    {
        UIManager = GameObject.Find("PopUpUI").GetComponent<UIManager>();

        buttons = GameObject.Find("Buttons");
        tooltipScreen = GameObject.Find("ToolTip Screen");

        tooltipScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void show_tooltip(int index) {
        index -= 1;

        tooltipScreen.transform.Find("Image").GetComponent<Image>().sprite = images[index];
        tooltipScreen.transform.Find("tooltip").GetComponent<Text>().text = toolTips[index];
        tooltipScreen.transform.Find("story").GetComponent<Text>().text = storys[index];
        tooltipScreen.transform.Find("name").GetComponent<Text>().text = names[index];

        UIManager.showingPopUp = true;
        isShowing = true;
        tooltipScreen.SetActive(isShowing);
        buttons.SetActive(!isShowing);
    }

    public void off_tooltip()
    {
        UIManager.showingPopUp = false;
        isShowing = false;
        tooltipScreen.SetActive(isShowing);
        buttons.SetActive(!isShowing);
    }
}
