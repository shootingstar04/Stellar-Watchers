using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPopUp : MonoBehaviour
{
    private GameObject itemPopUp;
    private UIManager UIManager;

    private Text name;
    private Text explanation;
    private Image image;

    public bool showing = false;

    // Start is called before the first frame update
    void Awake()
    {
        UIManager = GameObject.FindWithTag("UI").transform.Find("Screen UI").GetComponent<UIManager>();

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

    public void show_PopUP(CollectItem item) 
    {
        UIManager.show_popup();
        itemPopUp.SetActive(true);
        showing = true;

        Cursor.visible = true;
        Cursor.lockState =CursorLockMode.None;
        Time.timeScale = 0;

        switch (item.itemType) {
            case CollectItem.type.collect:
                name.text = ItemData.Instance.Name[item.itemNum];
                explanation.text = ItemData.Instance.Explanation[item.itemNum];
                image.sprite = ItemData.Instance.Image[item.itemNum];
                break;
            case CollectItem.type.skill:
                name.text = SkillData.Instance.Name[item.itemNum];
                explanation.text = SkillData.Instance.Explanation[item.itemNum];
                image.sprite = SkillData.Instance.Image[item.itemNum];
                break;
            case CollectItem.type.progress:
                name.text = ProgressData.Instance.Name[item.itemNum];
                explanation.text = ProgressData.Instance.Explanation[item.itemNum];
                image.sprite = ProgressData.Instance.Image[item.itemNum];
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
            UIManager.showingPopUp = false;

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            Time.timeScale = 1;
        }
    }
}
