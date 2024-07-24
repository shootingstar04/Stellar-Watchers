using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp_Boss_forDoorTesting : MonoBehaviour
{
    int hp = 10;
    bool isInvins = false;
    float invinsTimer = 0f;
    float maxInvinsDuration = 1f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(Define.PlayerTag))
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                damaged();
                isInvins = !isInvins;
            }

            if (isInvins)
            {
                invinsTimer += Time.deltaTime;
                if (invinsTimer >= maxInvinsDuration)
                {
                    isInvins = !isInvins;
                    invinsTimer = 0f;
                }
            }
        }
    }

    void damaged()
    {
        Debug.Log("보스맞음");
        Debug.Log(hp);
        if (isInvins)
        {
            return;
        }

        hp -= 5;
        if (hp <= 0)
        {
            dead();
        }
    }


    void dead()
    {
        BossDoor.bossdoorOpen();
        Destroy(this.gameObject);
    }
}
