using System;
using System.Collections;
using System.Collections.Generic;
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
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Define.PlayerTag))
        {
            switch (currentScene)
            {
                //case (nothuman):
                //{

                //}

            }

        }
    }
}
