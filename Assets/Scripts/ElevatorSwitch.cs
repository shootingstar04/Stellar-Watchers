using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorSwitch : MonoBehaviour
{



    //�÷��̾� ���� �ڵ忡 �߰�����
    public void SwitchFlick()
    {
        if(!Elevator.EV.isWorking)
        {
            Elevator.EV.Active();
        }
    }

    //�ӽ�
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(Input.GetKeyDown(KeyCode.P)) 
        {
            if (!Elevator.EV.isWorking)
            {
                Elevator.EV.Active();
            }
        }
    }
}
