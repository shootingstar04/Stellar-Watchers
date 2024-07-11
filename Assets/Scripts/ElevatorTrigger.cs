using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(Define.PlayerTag))// && !Elevator.EV.isWorking)
        {
            Debug.Log("¿¤º£ °¨Áö");
            Elevator.EV.Active();
        }
    }
}
