using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSP : MonoBehaviour
{
    public static PlayerSP instance;

    private int curSP;
    private int maxSP;

    public int CurSP
    {
        get
        {
            return curSP;
        }
    }
    public int MaxSP
    {
        get
        {
            return maxSP;
        }
    }
    void Awake()
    {
        if (!instance)
            instance = this;
        else
        {
            Destroy(this);
        }

        instance.curSP = 5;
        instance.maxSP = 5;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O)) modify_SP(1);
        if (Input.GetKeyDown(KeyCode.P)) modify_SP(-1);
        if (Input.GetKeyDown(KeyCode.L)) modify_max_SP(1);
    }

    public void modify_SP(int value)
    {
        curSP += value;
        if (curSP > maxSP) curSP = maxSP;
        else if (curSP < 0) curSP = 0;

        SPUI.Instance.refresh_sp(curSP);
    }
    public void modify_max_SP(int value)
    {
        maxSP += value;
        if (maxSP > 7) maxSP = 7;
        else if (maxSP < 1) maxSP = 1;

        curSP = maxSP;

        SPUI.Instance.add_max_SP(maxSP);
    }
}
