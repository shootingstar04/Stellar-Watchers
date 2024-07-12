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
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(Define.PlayerTag))
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("스위치 충돌");
                if (!Elevator.EV.isWorking && Elevator.EV.cage.transform.position.y == this.transform.position.y)
                {
                    Debug.Log("스위치 작동");
                    SwitchFlick();
                }
            }
        }
    }
}
