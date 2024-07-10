using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
    public Sprite Sprite1;
    public Sprite Sprite2;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<Image>().sprite = Sprite1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void change_image() 
    {
        if (this.GetComponent<Image>().sprite == Sprite1) this.GetComponent<Image>().sprite = Sprite2;
        else this.GetComponent<Image>().sprite = Sprite1;
    }
}
