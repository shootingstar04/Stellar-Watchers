using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemData : MonoBehaviour
{
    public static ItemData Instance;

    public List<Sprite> Image = new List<Sprite>();
    public List<bool> Acquired = new List<bool>();
    public List<string> Name = new List<string>();
    [TextArea]
    public List<string> Explanation = new List<string>();
    [TextArea]
    public List<string> Story = new List<string>();
    

    // Start is called before the first frame update
    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
