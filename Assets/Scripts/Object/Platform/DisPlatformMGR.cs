using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DisPlatform;

public class DisPlatformMGR : MonoBehaviour
{
    [SerializeField] private GameObject platform;

    [Header("ÇÃ·§Æû µîÀå Áö¿¬½Ã°£")]
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
