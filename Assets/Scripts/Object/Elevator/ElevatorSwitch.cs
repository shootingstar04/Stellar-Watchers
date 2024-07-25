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

    //�÷��̾� ���� �ڵ忡 �߰�����
    public void SwitchFlick()
    {
        if (!Elevator.EV.isWorking)
        {
            Debug.Log("���������� �۵��� x");
            if (!isThisSwitchUP)
            {
                if (!Mathf.Approximately(Elevator.EV.SwitchDown.transform.position.y, Elevator.EV.cage.transform.position.y))
                {
                    Debug.Log("���������� ȣ��");
                    Elevator.EV.Active();
                }
            }
            else
            {
                if (!Mathf.Approximately(Elevator.EV.SwitchUp.transform.position.y, Elevator.EV.cage.transform.position.y))
                {
                    Debug.Log("���������� ȣ��");
                    Elevator.EV.Active();
                }
            }

        }

    }

    private bool IsApproximatelyEqual(float a, float b, float tolerance)
    {
        return Mathf.Abs(a - b) < tolerance;
    }

    //�ӽ�
    /*
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(Define.PlayerTag))
        {
            if (Input.GetKey(KeyCode.P))
            {
                Debug.Log("����ġ �浹");
                if (!Elevator.EV.isWorking && Elevator.EV.cage.transform.position.y == this.transform.position.y)
                {
                    Debug.Log("����ġ �۵�");
                    SwitchFlick();
                }
            }
        }
    }
    */
}
