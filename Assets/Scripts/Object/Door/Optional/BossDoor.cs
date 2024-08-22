using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Security.Cryptography;

public class BossDoor : Door
{
    public static Action bossdoorOpen;
    public static Action bossdoorClose;

    private Collider2D col;

    [SerializeField] private GameObject Enterancedoor;
    [SerializeField] private GameObject ExitDoor;
    [SerializeField] private GameObject Boss;

    private void Awake()
    {
        col = GetComponent<Collider2D>();
        col.enabled= true;

        bossdoorOpen = () => OpenDoor();
        bossdoorClose = () => CloseDoor();
    }


    public override void OpenDoor()
    {
        Enterancedoor.GetComponent<triggerdoors>().OpenDoor();
        ExitDoor.GetComponent<triggerdoors>().OpenDoor();
    }

    public override void CloseDoor()
    {
       Enterancedoor.GetComponent<triggerdoors>().CloseDoor();
       ExitDoor.GetComponent<triggerdoors>().CloseDoor();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Define.PlayerTag))
        {
            bossdoorClose();
            col.enabled = false;
            StartCoroutine(SpawnBoss());
        }
    }

    IEnumerator SpawnBoss()
    {
        yield return new WaitForSeconds(1f);

        Instantiate(Boss, this.transform);
    }

    public void BossKilled()
    {
        bossdoorOpen();
    }

    public void ResetDoor()
    {
        col.enabled = true;
        OpenDoor();
        Destroy(this.transform.GetChild(2).gameObject);
    }
}
