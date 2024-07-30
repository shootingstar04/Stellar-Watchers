using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{

    public static DontDestroy thisIsPlayer;
    private bool hasInstance;
    public int numbering = 0;
    public bool isTeleported = false;

    private void Awake()
    {
        if (hasInstance)
        {
            Destroy(gameObject);
        }
        else
        {
            hasInstance = true;
            DontDestroyOnLoad(gameObject);
        }

        if (thisIsPlayer == null)
        {
            thisIsPlayer = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void GetNumbering(int num, bool temp)
    {
        numbering = num;
        isTeleported = temp;
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
