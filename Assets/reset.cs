using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class reset : MonoBehaviour
{
    private void Awake()
    {
        PlayerPrefs.SetInt(Define.sceneIndex, 2);
    }
}
