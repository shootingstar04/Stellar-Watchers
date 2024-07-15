using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectPopUp : MonoBehaviour
{
    [TextArea]
    public List<string> storys = new List<string>();
    [TextArea]
    public List<string> toolTips = new List<string>();
    public List<string> names = new List<string>();
    public List<Sprite> images = new List<Sprite>();

    public bool[] acquired = new bool[10];

    private GameObject tooltipScreen;
    private UIManager UIManager;

    private bool isShowing = false;

    // Start is called before the first frame update
    void Awake()
    {
        UIManager = GameObject.Find("Screen UI").GetComponent<UIManager>();

        tooltipScreen = GameObject.Find("Collection ToolTip");

        tooltipScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        check_input();
    }

    private void OnDisable()
    {
        off_tooltip();
    }

    private void check_input()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.X)) off_tooltip();
    }

    public void show_tooltip(int index)
    {
        index -= 1;

        if (acquired[index] && !isShowing)
        {

            tooltipScreen.transform.Find("Image").GetComponent<Image>().sprite = images[index];
            tooltipScreen.transform.Find("tooltip").GetComponent<Text>().text = toolTips[index];
            tooltipScreen.transform.Find("story").GetComponent<Text>().text = storys[index];
            tooltipScreen.transform.Find("name").GetComponent<Text>().text = names[index];

            UIManager.showingPopUp = true;
            isShowing = true;
            tooltipScreen.SetActive(isShowing);
        }
    }
    public void off_tooltip()
    {
        if (isShowing)
        {
            isShowing = false;
            tooltipScreen.SetActive(isShowing);
            this.transform.Find("Buttons").transform.Find("start").GetComponent<Button>().Select();
        }
    }

    public void set_button_image()
    {
        for (int i = 0; i < this.transform.Find("Buttons").transform.childCount; i++) {
            if(acquired[i])
                this.transform.Find("Buttons").transform.GetChild(i).GetComponent<ChangeImage>().set_image(0);
            else
                this.transform.Find("Buttons").transform.GetChild(i).GetComponent<ChangeImage>().set_image(1);
        }
    }
}