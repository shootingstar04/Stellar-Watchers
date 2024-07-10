using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ProgressExplanatioon : MonoBehaviour
{
    private GameObject buttonParent;

    private List<GameObject> buttons = new List<GameObject>();

    private Image currentImage;
    private Text currentName;
    private Text currentExplanation;

    [TextArea]
    public List<string> explanations = new List<string>();
    public List<string> names = new List<string>();
    public List<Sprite> images = new List<Sprite>();

    private int lastSelected = -1;

    private int gender = 1;//1 = male, -1 = female


    // Start is called before the first frame update
    void Awake()
    {
        buttonParent = GameObject.Find("Progress").transform.Find("Buttons").gameObject;

        for (int i = 0; i < buttonParent.transform.childCount; i++)
        {
            buttons.Add(buttonParent.transform.GetChild(i).gameObject);
        }

        currentExplanation = this.transform.Find("explanation").gameObject.GetComponent<Text>();
        currentImage = this.transform.Find("image").gameObject.GetComponent<Image>();
        currentName = this.transform.Find("name").gameObject.GetComponent<Text>();

        currentImage.sprite = images[0];
        currentExplanation.text = explanations[0];
        currentName.text = names[0];
    }

    // Update is called once per frame
    void Update()
    {
        set_explanation();
    }

    private void set_explanation()
    {
        int currentSelected = buttons.IndexOf(EventSystem.current.currentSelectedGameObject);

        if (currentSelected != -1) {
            if (currentSelected != lastSelected && currentSelected != buttons.Count - 1)
            {

                if (currentSelected == 0)
                {
                    if (gender == 1)
                    {
                        currentImage.sprite = images[currentSelected];
                        currentExplanation.text = explanations[currentSelected];
                        currentName.text = names[currentSelected];
                    }
                    else
                    {
                        currentImage.sprite = images[images.Count - 1];
                        currentExplanation.text = explanations[explanations.Count - 1];
                        currentName.text = names[names.Count - 1];
                    }
                }
                else
                {
                    currentImage.sprite = images[currentSelected];
                    currentExplanation.text = explanations[currentSelected];
                    currentName.text = names[currentSelected];
                }

                lastSelected = currentSelected;
            }

            else if (lastSelected == 0)
            {
                if (gender == 1)
                {
                    currentImage.sprite = images[lastSelected];
                    currentExplanation.text = explanations[lastSelected];
                    currentName.text = names[lastSelected];
                }
                else
                {
                    currentImage.sprite = images[images.Count - 1];
                    currentExplanation.text = explanations[explanations.Count - 1];
                    currentName.text = names[names.Count - 1];
                }

            }
        }
    }

    public void change_gender()
    {
        gender *= -1;
    }
}
