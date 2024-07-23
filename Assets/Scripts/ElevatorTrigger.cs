using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTrigger : MonoBehaviour
{
    bool reload = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!reload)
            return;

        if (other.CompareTag(Define.PlayerTag) && !Elevator.EV.isWorking)
        {
            reload = false;
            Debug.Log("¿¤º£ °¨Áö");
            other.transform.SetParent(Elevator.EV.transform);
            Elevator.EV.Active();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(Define.PlayerTag) && !Elevator.EV.isWorking)
        {
            reload = true;
            collision.transform.SetParent(null);
        }
    }
}
