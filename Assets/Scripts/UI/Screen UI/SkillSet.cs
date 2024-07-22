using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSet : MonoBehaviour
{
    private GameObject buttonParent;

    private List<GameObject> buttons = new List<GameObject>();

    private List<GameObject> skillSlot = new List<GameObject>();

    private enum skill 
    {
        none = -1,

        stellarStrike,
        pointSting,
        meteorliteSlash,
        galaxySlash,

        bigShot,
        speedyShot,
        laserAttack,
        stunShot
    }

    private skill[,] settedSkill = new skill[2, 2]
    {
        { skill.none, skill.none },
        { skill.none, skill.none }
    };

    // Start is called before the first frame update
    void Awake()
    {
        buttonParent = this.transform.Find("Buttons").gameObject;

        for (int i = 0; i < buttonParent.transform.childCount; i++)
        {
            buttons.Add(buttonParent.transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Set_skill(int skillNum)
    {
        if (SkillData.Instance.Acquired[skillNum])
        {
            for (int i = 0; i < 2; i++) {
                for (int j = 0; j < 2; j++)
                {
                    if (settedSkill[i, j] == (skill)skillNum)
                    {
                        settedSkill[i, j] = skill.none;
                        buttons[skillNum].GetComponent<ChangeImage>().set_image(0);
                        skillSlot[i * 2 + j].GetComponent<Image>().sprite = SkillData.Instance.Image[SkillData.Instance.Acquired.Count-1];
                        return;
                    }
                }
            }

            int gender = skillNum / 4;
            int askillNum= skillNum % 4;

            if (settedSkill[gender, 1] != skill.none)
            {
                if (settedSkill[gender, 0] != skill.none)
                {
                    settedSkill[gender, 0] = (skill)skillNum;
                    buttons[skillNum].GetComponent<ChangeImage>().set_image(1);
                    skillSlot[gender * 2].GetComponent<Image>().sprite = SkillData.Instance.Image[skillNum];
                }
                else
                {
                    settedSkill[gender, 1] = (skill)skillNum;
                    buttons[skillNum].GetComponent<ChangeImage>().set_image(1);
                    skillSlot[gender * 2 + 1].GetComponent<Image>().sprite = SkillData.Instance.Image[skillNum];
                }
            }
        }
    }

    public void set_button_image()
    {
        for (int i = 0; i < this.transform.Find("Buttons").transform.childCount; i++)
        {
            if (SkillData.Instance.Acquired[i])
                this.transform.Find("Buttons").transform.GetChild(i).GetComponent<ChangeImage>().set_image(0);
            else
                this.transform.Find("Buttons").transform.GetChild(i).GetComponent<ChangeImage>().set_image(2);
        }
    }
}
