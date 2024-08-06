using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillSet : MonoBehaviour
{
    private GameObject buttonParent;
    private List<GameObject> buttons = new List<GameObject>();

    private GameObject slotParent;
    private List<GameObject> skillSlot = new List<GameObject>();

    public enum skill 
    {
        none = -1,

        hardSwing,
        pointSting,
        smite,
        galaxySlash,

        meteorBomb,
        speedyShot,
        pillarOfLight,
        twinstars
    }

    public SkillSet.skill[,] settedSkill = new SkillSet.skill[2, 2];

    // Start is called before the first frame update
    void Awake()
    {
        settedSkill = SkillData.Instance.settedSkill;

        buttonParent = this.transform.Find("Buttons").gameObject;

        for (int i = 0; i < buttonParent.transform.childCount; i++)
        {
            buttons.Add(buttonParent.transform.GetChild(i).gameObject);
        }

        slotParent = this.transform.Find("Skill Slot").gameObject;

        for (int i = 0; i < slotParent.transform.childCount; i++)
        {
            skillSlot.Add(slotParent.transform.GetChild(i).gameObject);
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
                        Color ButtonColor = Color.white;
                        // Color ButtonColor = Color.white / 3;

                        buttons[skillNum].GetComponent<Image>().color = ButtonColor;

                        if (j == 1)
                        {
                            settedSkill[i, 1] = skill.none;
                            skillSlot[i * 2 + 1].GetComponent<Image>().sprite = SkillData.Instance.Image[SkillData.Instance.Acquired.Count - 1];
                        }
                        else if (j == 0)
                        {
                            settedSkill[i, 0] = settedSkill[i, 1];
                            settedSkill[i, 1] = skill.none;
                            skillSlot[i * 2].GetComponent<Image>().sprite = skillSlot[i * 2 + 1].GetComponent<Image>().sprite;
                            skillSlot[i * 2 + 1].GetComponent<Image>().sprite = SkillData.Instance.Image[SkillData.Instance.Acquired.Count - 1];
                        }

                        return;
                    }
                }
            }

            int gender = skillNum / 4;

            if (settedSkill[gender, 1] == skill.none)
            {
                if (settedSkill[gender, 0] == skill.none)
                {
                    settedSkill[gender, 0] = (skill)skillNum;
                    skillSlot[gender * 2].GetComponent<Image>().sprite = SkillData.Instance.Image[skillNum];
                }
                else
                {
                    settedSkill[gender, 1] = (skill)skillNum;
                    skillSlot[gender * 2 + 1].GetComponent<Image>().sprite = SkillData.Instance.Image[skillNum];
                }

                Color buttonColor = Color.red;
                //Color buttonColor = Color.white;

                buttons[skillNum].GetComponent<Image>().color = buttonColor;
            }
        }
    }

    public void set_button_image()
    {
        for (int i = 0; i < this.transform.Find("Buttons").transform.childCount; i++)
        {
            if (SkillData.Instance.Acquired[i])
                this.transform.Find("Buttons").transform.GetChild(i).GetComponent<Image>().sprite = SkillData.Instance.Image[i];
            else
                this.transform.Find("Buttons").transform.GetChild(i).GetComponent<Image>().sprite = SkillData.Instance.Image[SkillData.Instance.Acquired.Count - 1 ];
        }
    }
}

