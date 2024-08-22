using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMapLocation : MonoBehaviour
{
    public int MapNum;

    public void ChangeMapNum(int targetmap)
    {
        MapNum = targetmap;
    }
}
