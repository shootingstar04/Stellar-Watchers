using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillData : MonoBehaviour
{
    public static SkillData Instance;

    //[HideInInspector]
    public List<bool> Acquired = new List<bool>();


    public List<Sprite> Image = new List<Sprite>();
    public List<string> Name = new List<string>();
    [TextArea]
    public List<string> Explanation = new List<string>();


    public SkillSet.skill[,] settedSkill = new SkillSet.skill[2, 2]
    {
        { SkillSet.skill.none, SkillSet.skill.none },
        { SkillSet.skill.none, SkillSet.skill.none }
    };

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
}
