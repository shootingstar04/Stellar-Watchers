using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorSwitch : MonoBehaviour
{



    //플레이어 공격 코드에 추가예정
    public void SwitchFlick()
    {
        if(Elevator.EV.CurrentPos.y != this.transform.position.y)
        {
            Elevator.EV.Active();
        }
    }

    //임시
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Input.GetKeyDown(KeyCode.P)) 
        {
            if (!Elevator.EV.isWorking)
            {
                Debug.Log("스위치 작동");
                SwitchFlick();
            }
        }
    }
}
