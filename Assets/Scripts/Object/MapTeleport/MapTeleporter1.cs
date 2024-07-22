using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapTeleporter1 : MonoBehaviour
{
    [SerializeField] enum Map
    {
        nothuman = 1,
        nothuman_mapTPtest
    }
    private GameObject player;
    private string currentScene;
    private int currentMap;
    private int targetMap;

    [Header("��ǥ �ڷ���Ʈ ��ġ")]
    [Tooltip("�� ���� ���� ���� ���̸� true, ���� ���̸� fasle")]
    [SerializeField] bool isLeftRight;

    private void Awake()
    {
        /*
        player = GameObject.FindGameObjectWithTag(Define.PlayerTag);
        if(player== null)
            Instantiate(player);
        */

        currentScene = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        currentMap = (int)(Map)Enum.Parse(typeof(Map), currentScene);
        targetMap = currentMap;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(isLeftRight) //enum������ ���� ��
        {
            targetMap += 1;
        }
        else
        {
            targetMap -= 1;
        }
        Teleport(targetMap);
    }

    public void Teleport(int MapNum)
    {
        string target;
        target = toString((Map)MapNum);
        UnityEngine.SceneManagement.SceneManager.LoadScene(target);
    }

    string toString(Map thing)
    {
        return thing.ToString();
    }
}
