using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPUI : MonoBehaviour
{
    private GameObject currentHP;
    private GameObject HPBowl;

    private List<Transform> currentHPs = new List<Transform>();
    private List<Transform> HPBowls = new List<Transform>();

    private int currentHPValue = 5;
    private int maxHPValue = 5;

    public static HPUI Instance;
    // Start is called before the first frame update
    void Awake()
    {
        if (!Instance)
            Instance = this;
        else
        {
            Destroy(this);
        }


        currentHP = GameObject.Find("Current HP");
        HPBowl = GameObject.Find("HP Bowl");

        for (int i = 0; i < currentHP.transform.childCount; i++)
        {
            currentHPs.Add(currentHP.transform.GetChild(i));
            if (i >= maxHPValue) currentHPs[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < HPBowl.transform.childCount; i++)
        {
            HPBowls.Add(HPBowl.transform.GetChild(i));
            if (i >= maxHPValue) HPBowls[i].gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool refresh_hp(int HP) 
    {
        if (HP >= 0 && HP < maxHPValue + 1)
        {
            currentHPValue = HP;

            for (int i = 0; i < currentHP.transform.childCount; i++)
            {
                if (i < currentHPValue) currentHPs[i].gameObject.SetActive(true);
                else currentHPs[i].gameObject.SetActive(false);
            }

            return true;
        }
        else return false;
    }

    public void set_max_HP(int MaxHP) 
    {
        if (MaxHP < 9)
        {
            maxHPValue = MaxHP;

            refresh_hp(MaxHP);

            for (int i = 0; i < HPBowl.transform.childCount; i++)
            {
                if (i < maxHPValue) HPBowls[i].gameObject.SetActive(true);
                else HPBowls[i].gameObject.SetActive(false);
            }
        }
    }
}
