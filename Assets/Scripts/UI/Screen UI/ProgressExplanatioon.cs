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

        currentImage.sprite = ProgressData.Instance.Image[0];
        currentExplanation.text = ProgressData.Instance.Explanation[0];
        currentName.text = ProgressData.Instance.Name[0];
    }

    // Update is called once per frame
    void Update()
    {
        set_explanation();
    }

    private void set_explanation()
    {
        int currentSelected = buttons.IndexOf(EventSystem.current.currentSelectedGameObject);

        if (currentSelected != -1)
        {
            if (currentSelected != lastSelected && currentSelected != buttons.Count - 1)
            {
                if (currentSelected == 0)
                {
                    int count = ProgressData.Instance.reinforcementCount;
                    if (gender == 1)
                    {
                        currentImage.sprite = ProgressData.Instance.weaponImage[count];
                        currentExplanation.text = ProgressData.Instance.weaponExplanation[count];
                        currentName.text = ProgressData.Instance.weaponName[count];
                    }
                    else
                    {
                        currentImage.sprite = ProgressData.Instance.weaponImage[count + 4];
                        currentExplanation.text = ProgressData.Instance.weaponExplanation[count + 4];
                        currentName.text = ProgressData.Instance.weaponName[count + 4];
                    }
                }
                else if (ProgressData.Instance.Acquired[currentSelected - 1])
                {
                    currentImage.sprite = ProgressData.Instance.Image[currentSelected - 1];
                    currentExplanation.text = ProgressData.Instance.Explanation[currentSelected - 1];
                    currentName.text = ProgressData.Instance.Name[currentSelected - 1];
                }

                else
                {
                    int last = ProgressData.Instance.Acquired.Count - 1;
                    currentImage.sprite = ProgressData.Instance.Image[last];
                    currentExplanation.text = ProgressData.Instance.Explanation[last];
                    currentName.text = ProgressData.Instance.Name[last];
                }

                lastSelected = currentSelected;
            }

            else if (lastSelected == 0)
            {
                int count = ProgressData.Instance.reinforcementCount;
                if (gender == 1)
                {
                    currentImage.sprite = ProgressData.Instance.weaponImage[count];
                    currentExplanation.text = ProgressData.Instance.weaponExplanation[count];
                    currentName.text = ProgressData.Instance.weaponName[count];
                }
                else
                {
                    currentImage.sprite = ProgressData.Instance.weaponImage[count + 4];
                    currentExplanation.text = ProgressData.Instance.weaponExplanation[count + 4];
                    currentName.text = ProgressData.Instance.weaponName[count + 4];
                }
            }
        }
    }

    public void change_gender()
    {
        gender *= -1;
    }
}
