using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorSwitch : MonoBehaviour
{



    //플레이어 공격 코드에 추가예정
    public void SwitchFlick()
    {
        Elevator.EV.Active();
        
    }

    //임시
    private void OnCollisionEnter2D(Collision2D collision)
    {

        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("스위치 충돌");
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!Elevator.EV.isWorking)
            {
                Debug.Log("스위치 작동");
                SwitchFlick();
            }
        }
    }
}
