using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossDoor : Door
{
    public static Action bossdoorOpen;
    public static Action bossdoorClose;

    [SerializeField] private GameObject Enterancedoor;
    [SerializeField] private GameObject ExitDoor;

    private void Awake()
    {
        if (door == null)
        {
            door = GameObject.FindGameObjectWithTag("Boss");
        }

        bossdoorOpen = () => OpenDoor();
        bossdoorClose = () => CloseDoor();
    }

    private void Start()
    {
        if (door == null)
        {
            door = GameObject.FindGameObjectWithTag("Boss");
        }
    }

    public override void OpenDoor()
    {
        Destroy(Enterancedoor);
        Destroy(ExitDoor);
        Destroy(this.gameObject);
        
    }

    public override void CloseDoor()
    {
        Enterancedoor.SetActive(true);
        ExitDoor.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Define.PlayerTag))
        {
            bossdoorClose();
        }
    }
}
