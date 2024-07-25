using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressData : MonoBehaviour
{
    public static ProgressData Instance;

    [Header("����")]
    public List<Sprite> weaponImage = new List<Sprite>();
    public List<string> weaponName = new List<string>();
    [TextArea]
    public List<string> weaponExplanation = new List<string>();

    [Header("��Ÿ")]
    [Tooltip("�������� ȹ������ ���� ���")]
    public List<Sprite> Image = new List<Sprite>();
    public List<bool> Acquired = new List<bool>();
    public List<string> Name = new List<string>();
    [TextArea]
    public List<string> Explanation = new List<string>();



    public int stoneCount = 0;
    public int pointCount = 0;
    public int reinforcementCount = 0;


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
