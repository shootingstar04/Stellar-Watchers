using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ConstellationExplanation : MonoBehaviour
{
    private GameObject buttonParent;

    private List<GameObject> buttons = new List<GameObject>();

    private Image currentImage;
    private Text currentName;

    public List<string> names = new List<string>();
    public List<Sprite> images = new List<Sprite>();

    private int lastSelected = 0;

    // Start is called before the first frame update
    void Awake()
    {
        buttonParent = GameObject.Find("Constellation").transform.Find("Buttons").gameObject;

        for (int i = 0; i < buttonParent.transform.childCount; i++)
        {
            buttons.Add(buttonParent.transform.GetChild(i).gameObject);
        }

        currentImage = this.transform.Find("image").gameObject.GetComponent<Image>();
        currentName = this.transform.Find("name").gameObject.GetComponent<Text>();

        currentImage.sprite = images[0];
        currentName.text = names[0];
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

                lastSelected = currentSelected;
            }
        }
    }
}

