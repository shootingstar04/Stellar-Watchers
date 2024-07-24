using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        Debug.Log("priviousMap = " + priviousMap);
    }

    public bool giveLeftRight()
    {
        return priviousMap;
    }


    /*
    private void OnLevelWasLoaded(int level)
    {
        GameObject[] teleporters = GameObject.FindGameObjectsWithTag(Define.TPtag);
        foreach (GameObject teleporter in teleporters)
        {
            bool LR = MapTeleporter1.GiveIsLR();
            Debug.Log(teleporter.name + ", "+ LR);
            if(priviousMap != LR)
            {
                Debug.Log(teleporter.transform.position);
                this.transform.position = new Vector3 (teleporter.transform.position.x + offset, teleporter.transform.position.y, this.transform.position.z);
                break;
            }
        }
    }
    */

    /*
    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject[] teleporters = GameObject.FindGameObjectsWithTag(Define.TPtag);
        foreach (GameObject teleporter in teleporters)
        {
            bool LR = MapTeleporter1.GiveIsLR();
            Debug.Log(teleporter.name + ", " + LR);
            if (priviousMap != LR)
            {
                Debug.Log(teleporter.transform.position);
                this.transform.position = new Vector3(teleporter.transform.position.x + offset, teleporter.transform.position.y, this.transform.position.z);
                break;
            }
        }
    }
    */
}
