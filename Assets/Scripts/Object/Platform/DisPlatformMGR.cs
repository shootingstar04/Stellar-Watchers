using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DisPlatform;

public class DisPlatformMGR : MonoBehaviour
{
    [SerializeField] private GameObject platform;

    private float ReappearTime = 2f;
    private float timer = 0f;

    // Update is called once per frame
    void Update()
    {

        if(!platform.activeInHierarchy)
        {
            timer += Time.deltaTime;
            if(timer >= ReappearTime) 
            {
                timer = 0f;
                platform.SetActive(true);
            }
        }

    }

}
