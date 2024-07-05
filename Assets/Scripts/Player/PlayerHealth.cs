using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    public int CurHealth;
    public int MaxHealth;

    void Awake()
    {   
        instance = this;
    }
}
