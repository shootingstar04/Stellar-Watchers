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

    private int lastSelected = -1;

    // Start is called before the first frame update
    void Awake()
    {
        buttonParent = GameObject.Find("Astrology").transform.Find("Buttons").gameObject;

        for (int i = 0; i < buttonParent.transform.childCount; i++)
        {
            buttons.Add(buttonParent.transform.GetChild(i).gameObject);
        }

        currentImage = this.transform.Find("image").gameObject.GetComponent<Image>();
        currentName = this.transform.Find("name").gameObject.GetComponent<Text>();
        currentExplanation = this.transform.Find("explanation").gameObject.GetComponent<Text>();

        if (SkillData.Instance.Acquired[0])
        {
            currentImage.sprite = SkillData.Instance.Image[0];
            currentName.text = SkillData.Instance.Name[0];
            currentExplanation.text = SkillData.Instance.Explanation[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        set_explanation();
    }

    public void set_explanation()
    {
        int currentSelected = buttons.IndexOf(EventSystem.current.currentSelectedGameObject);

        if (currentSelected != -1)
        {
            if (currentSelected != lastSelected)
            {

                if (SkillData.Instance.Acquired[currentSelected])
                {
                    currentImage.sprite = SkillData.Instance.Image[currentSelected];
                    currentName.text = SkillData.Instance.Name[currentSelected];
                    currentExplanation.text = SkillData.Instance.Explanation[currentSelected];
                }

                else
                {
                    int none = SkillData.Instance.Acquired.Count-1;
                    currentImage.sprite = SkillData.Instance.Image[none];
                    currentName.text = SkillData.Instance.Name[none];
                    currentExplanation.text = SkillData.Instance.Explanation[none];
                }

                lastSelected = currentSelected;
            }
        }
    }
}
