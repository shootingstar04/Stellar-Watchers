using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisPlatform : MonoBehaviour
{
    private bool isTriggered = false;
    private float DisappearTime = 1f;
    private float timer = 0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isTriggered = true; 
        }
    }

    private void OnTriggerStay2D(Collider2D collision) //if dissapeared -> remove sprite && set collider to Trigger instead of SetActive(false). from there find if player is inside of collider
        //if this method not possible -> use update && raycast/overlapBox in displatformMgr
    {
        if(collision.gameObject.CompareTag(Define.PlayerTag))
        {

        }
    }
}
