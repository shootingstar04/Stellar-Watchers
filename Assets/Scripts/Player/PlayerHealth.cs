using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    private int CurHP;
    private int MaxHP;

    void Awake()
    {
        if (!instance)
            instance = this;
        else
        {
            Destroy(this);
        }

        instance.CurHP = 5;
        instance.MaxHP = 5;
    }

    private void Update()
    {
    }

    public void modify_HP(int value)
    {
        CurHP += value;
        if (CurHP > MaxHP) CurHP = MaxHP;
        else if (CurHP < 0) CurHP = 0;

        HPUI.Instance.refresh_hp(CurHP);
    }
    public void modify_max_HP(int value)
    {
        MaxHP += value;
        if (MaxHP > 8) MaxHP = 8;
        else if (MaxHP < 1) MaxHP = 1;

        CurHP = MaxHP;

        HPUI.Instance.add_max_HP(MaxHP);
    }
}
