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
    private void OnCollisionEnter2D(Collision2D collision)
    {

        
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        Debug.Log("����ġ �浹");
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (!Elevator.EV.isWorking)
            {
                Debug.Log("����ġ �۵�");
                SwitchFlick();
            }
        }
    }
}
