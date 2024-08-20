using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    private int curHP;
    private int maxHP;

    private PlayerMove playerMove;
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

        playerMove = this.GetComponent<PlayerMove>();

        instance.curHP = 5;
        instance.maxHP = 5;
    }

    private void Update()
    {
    }


    public void modify_HP(int value)
    {
        if (value < 0)
        {
            playerMove.Hitted(value == -2 ? 5 : 1);
        }

        curHP += value;
        if (curHP > maxHP) curHP = maxHP;
        else if (curHP < 0) curHP = 0;

        HPUI.Instance.refresh_hp(CurHP);
    }

    public void modify_HP(int value, float force)
    {
        if (value < 0)
        {
            playerMove.Hitted(force);
        }

        curHP += value;
        if (curHP > maxHP) curHP = maxHP;
        else if (curHP < 0) curHP = 0;

        HPUI.Instance.refresh_hp(CurHP);
    }

    public void set_HP(int value)
    {
        curHP = value;
        if (curHP > maxHP) curHP = maxHP;
        else if (curHP < 0) curHP = 0;

        HPUI.Instance.refresh_hp(CurHP);
    }

    public void set_max_HP(int value)
    {

        maxHP = value;
        if (maxHP > 8) maxHP = 8;
        else if (maxHP < 1) maxHP = 1;

        HPUI.Instance.set_max_HP(maxHP);
    }

    public void modify_max_HP(int value)
    {
        maxHP += value;
        if (maxHP > 8) maxHP = 8;
        else if (maxHP < 1) maxHP = 1;

        curHP = maxHP;

        HPUI.Instance.set_max_HP(maxHP);
    }
}
