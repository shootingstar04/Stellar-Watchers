using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ConstellationPopUp : MonoBehaviour
{
    public List<string> constellationNames = new List<string>();
    public List<Sprite> constellationImages = new List<Sprite>();
    public List<bool> acquired = new List<bool>();

    private List<GameObject> buttonParents = new List<GameObject>();
    private List<GameObject> buttons = new List<GameObject>();

    private GameObject constellationPopUp;
    private GameObject explanation_BG;

    private UIManager UIManager;

    private bool isShowing = false;


    public List<string> starNames = new List<string>();
    [TextArea]
    public List<string> starFeatures = new List<string>();
    [TextArea]
    public List<string> starExplanations = new List<string>();


    private Text currentName;
    private Text currentExplanation;
    private Text currentFeature;

    private int lastSelected = -1;

    // Start is called before the first frame update
    void Awake()
    {
        UIManager = GameObject.Find("Screen UI").GetComponent<UIManager>();

        constellationPopUp = GameObject.Find("Constellation PopUp");

        explanation_BG = constellationPopUp.transform.Find("BackGround").transform.Find("BG1").gameObject;

        currentExplanation = explanation_BG.transform.Find("explanation").GetComponent<Text>();
        currentName = explanation_BG.transform.Find("name").GetComponent<Text>();
        currentFeature = explanation_BG.transform.Find("feature").GetComponent<Text>();

        for (int i = 0; i < constellationPopUp.transform.Find("Buttons").transform.childCount; i++)
        {
            buttonParents.Add(constellationPopUp.transform.Find("Buttons").transform.GetChild(i).gameObject);
            buttonParents[i].SetActive(false);
        }

        for (int i = 0; i < buttonParents.Count; i++)
        {
            for (int j = 0; j < buttonParents[i].transform.childCount; j++)
            {
                buttons.Add(buttonParents[i].transform.GetChild(j).gameObject);
            }
        }

        constellationPopUp.SetActive(false);


        for (int i = 0; i < buttons.Count; i++)
        {
            if (acquired[i]) buttons[i].GetComponent<ChangeImage>().set_image(0);
            else buttons[i].GetComponent<ChangeImage>().set_image(1);
        }
    }
    void Update()
    {
        check_input();
        set_explanation();
    }



    private void check_input()
    {
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.X)) off_popup();
    }

    public void show_popup(int index)
    {
        if (!isShowing)
        { 

            index -= 1;

            constellationPopUp.transform.Find("ConstellaImage").GetComponent<Image>().sprite = constellationImages[index];
            constellationPopUp.transform.Find("ConstellaName").GetComponent<Text>().text = constellationNames[index];

            UIManager.showingPopUp = true;
            isShowing = true;
            constellationPopUp.SetActive(true);
            buttonParents[index].SetActive(true);
            buttonParents[index].transform.Find("start").GetComponent<Button>().Select();

            for (int i = 0; i < buttons.Count; i++)
            {
                if (acquired[i]) buttons[i].GetComponent<ChangeImage>().set_image(0);
                else buttons[i].GetComponent<ChangeImage>().set_image(1);
            }
        }
    }

    public void off_popup()
    {
        if (isShowing)
        {
            for (int i = 0; i < buttonParents.Count; i++)
            {
                buttonParents[i].SetActive(false);
            }
            UIManager.showingPopUp = false;
            isShowing = false;
            constellationPopUp.SetActive(false);
            this.transform.Find("Buttons").transform.Find("start").GetComponent<Button>().Select();
        }
    }

    public void set_explanation()
    {
        int currentSelected = buttons.IndexOf(EventSystem.current.currentSelectedGameObject);

        if (currentSelected != -1)
        {
            if (currentSelected != lastSelected && acquired[currentSelected])
            {
                currentExplanation.text = starExplanations[currentSelected];
                currentName.text = starNames[currentSelected];
                currentFeature.text = starFeatures[currentSelected];

                lastSelected = currentSelected;
            }
        }
    }
}
