using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSP : MonoBehaviour
{
    public static PlayerSP instance;

    public int CurSP;
    public int MaxSP;

    void Awake()
    {
        instance = this;
    }
}
