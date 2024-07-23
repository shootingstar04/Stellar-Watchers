using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Define.PlayerTag))
        {
            TriggerDoor door = GetComponentInChildren<TriggerDoor>();
            door.CloseDoor();
        }
    }
}
