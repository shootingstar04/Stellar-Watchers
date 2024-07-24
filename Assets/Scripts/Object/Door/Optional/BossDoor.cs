using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BossDoor : Door
{
    public static Action bossdoorOpen;
    public static Action bossdoorClose;

    private void Awake()
    {
        if (door.Count == 0)
        {
            CollectChildObjects(this.gameObject);
        }

        bossdoorOpen = () => OpenDoor();
        bossdoorClose = () => CloseDoor();
    }

    void CollectChildObjects(GameObject parent)
    {
        foreach (Transform child in parent.transform)
        {
            if(child.GetComponent<EnteranceTrigger>() == null)
            door.Add(child.gameObject);
        }
    }

    public override void OpenDoor()
    {
        isDisabled = true;
        Destroy(this.gameObject.transform.GetComponentInChildren<EnteranceTrigger>());
        foreach (GameObject obj in door)
        {
            obj.SetActive(false);
        }
    }

    public override void CloseDoor()
    {
        foreach(GameObject obj in door)
        {
            obj.SetActive(true);
        }
    }
}
