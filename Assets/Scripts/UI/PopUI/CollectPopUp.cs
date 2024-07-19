using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectPopUp : MonoBehaviour
{

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

        if (ItemData.Instance.Acquired[index] && !isShowing)
        {
            Debug.Log(tooltipScreen.name + ItemData.Instance.Name[0]);
            tooltipScreen.transform.Find("Image").GetComponent<Image>().sprite = ItemData.Instance.Image[index];
            tooltipScreen.transform.Find("tooltip").GetComponent<Text>().text = ItemData.Instance.Explanation[index];
            tooltipScreen.transform.Find("story").GetComponent<Text>().text = ItemData.Instance.Story[index];
            tooltipScreen.transform.Find("name").GetComponent<Text>().text = ItemData.Instance.Name[index];

            UIManager.show_popup();
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
            if(ItemData.Instance.Acquired[i])
                this.transform.Find("Buttons").transform.GetChild(i).GetComponent<ChangeImage>().set_image(0);
            else
                this.transform.Find("Buttons").transform.GetChild(i).GetComponent<ChangeImage>().set_image(1);
        }
    }
}