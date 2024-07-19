using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillExplanation : MonoBehaviour
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

    private int lastSelected = 0;

    public static SkillExplanation skillExplanation;

    // Start is called before the first frame update
    void Awake()
    {
        if(skillExplanation)
            skillExplanation = this;

        buttonParent = GameObject.Find("Astrology").transform.Find("Buttons").gameObject;

        for (int i = 0; i < buttonParent.transform.childCount; i++)
        {
            buttons.Add(buttonParent.transform.GetChild(i).gameObject);
        }

        currentImage = this.transform.Find("image").gameObject.GetComponent<Image>();
        currentName = this.transform.Find("name").gameObject.GetComponent<Text>();
        currentExplanation = this.transform.Find("explanation").gameObject.GetComponent<Text>();

        currentImage.sprite = images[0];
        currentName.text = names[0];
        currentExplanation.text = explanations[0];
    }

    // Update is called once per frame
    void Update()
    {
        set_image();
    }

    public void set_image()
    {
        int currentSelected = buttons.IndexOf(EventSystem.current.currentSelectedGameObject);

        if (currentSelected != -1)
        {
            if (currentSelected != lastSelected)
            {
                currentImage.sprite = images[currentSelected];
                currentName.text = names[currentSelected];
                currentExplanation.text = explanations[currentSelected];

                lastSelected = currentSelected;
            }
        }
    }
}
