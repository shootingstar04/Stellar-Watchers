using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    private int curHP;
    private int maxHP;

    public int CurHP
    {
        get
        {
            return curHP;
        }
    }
    public int MaxHP
    {
        get
        {
            return maxHP;
        }
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
            instance = this;
        }

        instance.curHP = 5;
        instance.maxHP = 5;
    }

    private void Update()
    {
    }


    public void modify_HP(int value)
    {
        curHP += value;
        if (curHP > maxHP) curHP = maxHP;
        else if (curHP < 0) curHP = 0;

        HPUI.Instance.refresh_hp(CurHP);
    }
    public void modify_max_HP(int value)
    {
        maxHP += value;
        if (maxHP > 8) maxHP = 8;
        else if (maxHP < 1) maxHP = 1;

        curHP = maxHP;

        HPUI.Instance.add_max_HP(maxHP);
    }
}
