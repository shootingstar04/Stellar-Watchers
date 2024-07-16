using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPopUp : MonoBehaviour
{
    [TextArea]
    public List<string> collectExplanations = new List<string>();
    public List<string> collectNames = new List<string>();
    public List<Sprite> collectImages = new List<Sprite>();

    [TextArea]
    public List<string> starExplanations = new List<string>();
    public List<string> starNames = new List<string>();
    public List<Sprite> starImages = new List<Sprite>();

    [TextArea]
    public List<string> keyExplanations = new List<string>();
    public List<string> keyNames = new List<string>();
    public List<Sprite> keyImages = new List<Sprite>();

    private GameObject itemPopUp;
    private UIManager UIManager;

    private Text name;
    private Text explanation;
    private Image image;

    public bool showing = false;

    // Start is called before the first frame update
    void Awake()
    {
        UIManager = GameObject.Find("Screen UI").GetComponent<UIManager>();

        itemPopUp = GameObject.Find("Item PopUp");
        itemPopUp.SetActive(false);

        name = itemPopUp.transform.Find("Name").GetComponent<Text>();
        explanation = itemPopUp.transform.Find("Explanation").GetComponent<Text>();
        image = itemPopUp.transform.Find("Image").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        check_input();
    }

    private void check_input()
    {
        if (Input.GetKeyDown(KeyCode.X)) off_PopUp();
        else if (Input.GetKeyDown(KeyCode.Escape)) off_PopUp();
    }

    public void show_PopUP(int itemType, int itemNum) 
    {
        Debug.Log(itemType);

        UIManager.show_popup();
        itemPopUp.SetActive(true);
        showing = true;

        Cursor.visible = true;
        Cursor.lockState =CursorLockMode.None;
        Time.timeScale = 0;

        switch (itemType) {
            case 0:
                name.text = collectNames[itemNum];
                explanation.text = collectExplanations[itemNum];
                image.sprite = collectImages[itemNum];
                break;
            case 1:
                name.text = starNames[itemNum];
                explanation.text = starExplanations[itemNum];
                image.sprite = starImages[itemNum];
                break;
            case 2:
                name.text = keyNames[itemNum];
                explanation.text = keyExplanations[itemNum];
                image.sprite = keyImages[itemNum];
                break;
        }

        itemPopUp.transform.Find("Confirm").GetComponent<Button>().Select();

        return;
    }

    public void off_PopUp()
    {
        if (showing)
        {
            itemPopUp.SetActive(false);
            showing = false;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }
    }
}
