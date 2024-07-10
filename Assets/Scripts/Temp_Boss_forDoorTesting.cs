using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp_Boss_forDoorTesting : MonoBehaviour
{
    int hp = 10;
    Door door;

    void damaged()
    {

    }


    void dead()
    {
        door.OpenDoor();
    }
}
