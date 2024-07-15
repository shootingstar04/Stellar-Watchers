using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorSwitch : MonoBehaviour
{
    public static ElevatorSwitch EVSwitch;

    private void Awake()
    {
        EVSwitch = this;
    }

    //�÷��̾� ���� �ڵ忡 �߰�����
    public void SwitchFlick()
    {
        if (!Elevator.EV.isWorking && !Mathf.Approximately(Elevator.EV.cage.transform.position.y, this.transform.position.y))
        {
            Elevator.EV.Active();
        }

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
