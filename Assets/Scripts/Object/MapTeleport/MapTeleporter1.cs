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

    public static MapTeleporter1 mapTP;

    private void Awake()
    {
        /*
        player = GameObject.FindGameObjectWithTag(Define.PlayerTag);
        if(player== null)
            Instantiate(player);
        */
        mapTP = this;
        currentScene = SceneManager.GetActiveScene().name;

        currentMap = (int)(Map)Enum.Parse(typeof(Map), currentScene);
        Debug.Log(currentScene +" = " + currentMap);
        targetMap = currentMap;
        Debug.Log("targetMap = " + targetMap);
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
        Debug.Log(targetMap);
        Teleport(targetMap);
    }

    public void Teleport(int MapNum)
    {
        string target;
        target = toString((Map)MapNum);
        DontDestroy.thisIsPlayer.getLeftRight(isLeftRight);
        SceneManager.LoadScene(target);
    }

    string toString(Map thing)
    {
        return thing.ToString();
    }

    public static bool GiveIsLR()
    {
        return MapTeleporter1.mapTP.isLeftRight;
    }
}
