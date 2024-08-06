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

    private int currentGold = 0;

    public int CurrentGold
    {
        get
        {
            return currentGold;
        }
    }


    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance.gameObject);
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void modify_gold(int gold)
    {
        currentGold += gold;
        if (currentGold > 999999999) currentGold = 999999999;
    }
}
