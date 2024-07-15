using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeMap : MonoBehaviour
{
    enum MapCode
    {
        nothuman = 1,
        nothuman_mapTPtest
    }

    MapCode currentScene;

    private void Start()
    {
        currentScene = (MapCode)Enum.Parse(typeof(MapCode), SceneManager.GetActiveScene().name);

        Debug.Log(currentScene); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Define.PlayerTag))
        {
            switch (currentScene)
            {
                case MapCode.nothuman:
                    {
                        Debug.Log("nothuman");
                    }
                    break;
                case MapCode.nothuman_mapTPtest:
                    {
                        Debug.Log("nothuman_mapTest");
                    }
                    break;
                default:
                    {
                        Debug.Log("Error");
                    }
                    break;

            }

        }
    }
}
