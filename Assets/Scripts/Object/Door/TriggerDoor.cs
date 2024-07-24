using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDoor : Door
{
    [SerializeField] Collider2D trigger;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Define.PlayerTag))
        {
            GameObject door = this.gameObject.transform.GetChild(0).gameObject;
            door.SetActive(true);
        }
        
    }

    public override void OpenDoor()
    {
        Debug.Log("dasd");
    }

    public override void CloseDoor()
    {
    }

}
