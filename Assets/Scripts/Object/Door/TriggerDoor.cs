using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TriggerDoor : Door
{
    enum Trigger
    {
        Error = 0,
        Boss,
        Challenge,
        Timeattack,
        Exit
    }

    [SerializeField] Trigger typeOfDoor;

    [SerializeField] Collider2D trigger;

    [SerializeField] List<GameObject> switches;
    
    private void Awake()
    {
        if(door == null)
            door = this.gameObject.transform.GetChild(0).gameObject;

        switches = new List<GameObject>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Define.PlayerTag))
        {
            CloseDoor();
        }
        
    }

    public override void OpenDoor()
    {
        Debug.Log("������ �Լ� ȣ���� ������Ʈ : " + this.gameObject.name);
        door.SetActive(false);
    }

    public override void CloseDoor()
    {
        Debug.Log("������ �Լ� ȣ���� ������Ʈ : " + this.gameObject.name);
        door.SetActive(true);
    }

    public void CheckSwitches()
    {
        if(switches.Count == 0)
        {
            OpenDoor();
        }
    }

}
