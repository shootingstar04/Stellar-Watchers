using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{

    public static DontDestroy thisIsPlayer;
    private bool priviousMap;
    private sbyte offset = 0;

    private void Awake()
    {
        thisIsPlayer = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void getLeftRight(bool isLR)
    {
        if (isLR)
        {
            offset = 1;
        }
        else
        {
            offset = -1;
        }
        priviousMap = isLR;
    }

    private void OnLevelWasLoaded(int level)
    {
        GameObject[] teleporters = GameObject.FindGameObjectsWithTag(Define.TPtag);
        foreach (GameObject teleporter in teleporters)
        {
            bool LR = MapTeleporter1.GiveIsLR();
            if(priviousMap != LR)
            {
                
                this.transform.position = new Vector3 (teleporter.transform.position.x + offset, teleporter.transform.position.y, this.transform.position.z);
            }
        }
    }
}
