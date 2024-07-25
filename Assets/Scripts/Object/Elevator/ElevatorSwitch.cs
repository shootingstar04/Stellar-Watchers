using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ElevatorSwitch : MonoBehaviour
{
    public static ElevatorSwitch EVSwitch;

    [SerializeField] bool isThisSwitchUP;

    float tolerance = 0.0000001f;

    private Transform obj;
    private Transform EV;
    private Elevator elevator;

    private void Awake()
    {
        obj = this.transform.parent;
        EV  = obj.transform.parent;

        List<Transform> gameObjects = new List<Transform>();
        foreach (Transform child in EV.transform)
        {
            gameObjects.Add(child);
        }

        foreach (Transform obj in gameObjects)
        {
            if (obj.GetComponent<Elevator>())
            {
                elevator = obj.GetComponent<Elevator>();
            }
        }
        Debug.Log(elevator);
    }

    //플레이어 공격 코드에 추가예정
    public void SwitchFlick()
    {
        if (!elevator.isWorking)
        {
            Debug.Log("엘리베이터 작동중 x");
            if (!isThisSwitchUP)
            {
                if (!Mathf.Approximately(elevator.SwitchDown.transform.position.y, elevator.cage.transform.position.y))
                {
                    Debug.Log("엘리베이터 호출");
                    elevator.Active();
                }
            }
            else
            {
                if (!Mathf.Approximately(elevator.SwitchUp.transform.position.y, elevator.cage.transform.position.y))
                {
                    Debug.Log("엘리베이터 호출");
                    elevator.Active();
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
