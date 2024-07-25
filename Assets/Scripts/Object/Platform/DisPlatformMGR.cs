using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DisPlatform;

public class DisPlatformMGR : MonoBehaviour
{
    [SerializeField] private GameObject platform;

    [Header("�÷��� ���� �����ð�")]
    [SerializeField] private float ReappearTime = 2f;

    public void callInvoke()
    {
        Invoke("callTimer", ReappearTime);
    }

    void callTimer()
    {
        platform.SetActive(true);
    }

}
