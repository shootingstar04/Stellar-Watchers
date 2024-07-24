using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Door : MonoBehaviour
{
    [SerializeField] protected List<GameObject> door;
    [SerializeField] protected bool isDisabled = false;

    public abstract void OpenDoor();
    public abstract void CloseDoor();


}
