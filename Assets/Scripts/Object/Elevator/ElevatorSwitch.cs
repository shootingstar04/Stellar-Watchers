using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ElevatorSwitch : MonoBehaviour
{
    public static ElevatorSwitch EVSwitch;

    [SerializeField] bool isThisSwitchUP;

    float tolerance = 0.0000001f;

    private void Awake()
    {
        EVSwitch = this;
    }

    //플레이어 공격 코드에 추가예정
    public void SwitchFlick()
    {
        if (!Elevator.EV.isWorking)
        {
            Debug.Log("엘리베이터 작동중 x");
            if (!isThisSwitchUP)
            {
                if (!Mathf.Approximately(Elevator.EV.SwitchDown.transform.position.y, Elevator.EV.cage.transform.position.y))
                {
                    Debug.Log("엘리베이터 호출");
                    Elevator.EV.Active();
                }
            }
            else
            {
                if (!Mathf.Approximately(Elevator.EV.SwitchUp.transform.position.y, Elevator.EV.cage.transform.position.y))
                {
                    Debug.Log("엘리베이터 호출");
                    Elevator.EV.Active();
                }
            }

        }

    }

    private bool IsApproximatelyEqual(float a, float b, float tolerance)
    {
        return Mathf.Abs(a - b) < tolerance;
    }

    //임시
    /*
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(Define.PlayerTag))
        {
            if (Input.GetKey(KeyCode.P))
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
    */
}
