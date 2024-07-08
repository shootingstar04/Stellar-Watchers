using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    public int CurHP;
    public int MaxHP;

    void Awake()
    {
        instance = this;
    }
}
