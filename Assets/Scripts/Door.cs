using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] Collider2D enterTrigger;



    // Start is called before the first frame update
    void Start()
    {
        if(door.CompareTag("Exit"))
        {
            enterTrigger = null;
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Define.PlayerTag))
        {
            door.SetActive(true);
        }
    }

    public void OpenDoor()
    {
        door.SetActive(false);
        if(door.gameObject.CompareTag("Enter"))
        {
            Destroy(enterTrigger);
         }
    }
}
