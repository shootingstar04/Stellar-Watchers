using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
    public List<Sprite> sprites = new List<Sprite>();

    private int imageIndex = 0;

    // Start is called before the first frame update
    void Awake()
    {
        this.GetComponent<Image>().sprite = sprites[0];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void change_image() 
    {
        imageIndex += 1;
        if (imageIndex > sprites.Count - 1) imageIndex = 0;

        this.GetComponent<Image>().sprite = sprites[imageIndex];
    }

    public void set_image(int index)
    {
        imageIndex = index;
        this.GetComponent<Image>().sprite = sprites[index];
    }
}
