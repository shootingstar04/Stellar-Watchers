using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorSwitch : MonoBehaviour
{



    //�÷��̾� ���� �ڵ忡 �߰�����
    public void SwitchFlick()
    {
        Elevator.EV.Active();

    }

    //�ӽ�
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag(Define.PlayerTag))
        {
            if (Input.GetKeyDown(KeyCode.P))
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
}
