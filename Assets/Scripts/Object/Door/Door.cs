using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Door : MonoBehaviour
{
    [SerializeField] protected GameObject door;

    public abstract void OpenDoor();
    public abstract void CloseDoor();


}
