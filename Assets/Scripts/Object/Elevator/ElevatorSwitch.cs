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

    //�÷��̾� ���� �ڵ忡 �߰�����
    public void SwitchFlick()
    {
        if (!elevator.isWorking)
        {
            Debug.Log("���������� �۵��� x");
            if (!isThisSwitchUP)
            {
                if (!Mathf.Approximately(elevator.SwitchDown.transform.position.y, elevator.cage.transform.position.y))
                {
                    Debug.Log("���������� ȣ��");
                    elevator.Active();
                }
            }
            else
            {
                if (!Mathf.Approximately(elevator.SwitchUp.transform.position.y, elevator.cage.transform.position.y))
                {
                    Debug.Log("���������� ȣ��");
                    elevator.Active();
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
