using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTrigger : MonoBehaviour
{
    bool reload = true;

    private Transform EV;
    private Elevator elevator;

    private void Awake()
    {
        EV = this.transform.parent;
        elevator = EV.GetComponent<Elevator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!reload)
            return;

        if (other.CompareTag(Define.PlayerTag) && !elevator.isWorking)
        {
            reload = false;
            Debug.Log("¿¤º£ °¨Áö");
            other.transform.SetParent(elevator.transform);
            elevator.Active();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(Define.PlayerTag) && !elevator.isWorking)
        {
            reload = true;
            collision.transform.SetParent(null);
        }
    }
}
