using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp_Boss_forDoorTesting : MonoBehaviour
{
    int hp = 10;
    [SerializeField] Door doorEnter;
    [SerializeField] Door doorExit;
    bool isInvins = false;
    float invinsTimer = 0f;
    float maxInvinsDuration = 1f;

    private void Awake()
    {

    }


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            damaged();
            isInvins = !isInvins;
        }

        if(isInvins)
        {
            invinsTimer += Time.deltaTime;
            if(invinsTimer >= maxInvinsDuration )
            {
                isInvins = !isInvins;
                invinsTimer = 0f;
            }
        }
    }

    void damaged()
    {
        Debug.Log("보스맞음");
        Debug.Log(hp);
        if(isInvins)
        {
            return;
        }

        hp -= 5;
        if(hp<=0)
        {
            dead();
        }
    }


    void dead()
    {
        doorEnter.OpenDoor();
        doorExit.OpenDoor();
        Destroy(this.gameObject);
    }
}
