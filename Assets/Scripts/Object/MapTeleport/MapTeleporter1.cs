using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapTeleporter1 : MonoBehaviour
{
    private GameObject player;
    private Scene currentScene;
    private int currentIndex;
    private int targetIndex;

    [Header("목표 텔레포트 위치")]
    [Tooltip("맵 순서 따라 다음 맵이면 true, 이전 맵이면 fasle")]
    [SerializeField] bool isLeftRight;

    private static bool returnLR;

    public static MapTeleporter1 mapTP;

    private void Awake()
    {
        /*
        player = GameObject.FindGameObjectWithTag(Define.PlayerTag);
        if(player== null)
            Instantiate(player);
        */
        mapTP = this;
        currentScene = SceneManager.GetActiveScene();
        currentIndex = currentScene.buildIndex;
        targetIndex = currentIndex;
        Debug.Log(currentIndex + "는 현재 buildindex");

        returnLR = isLeftRight;

        Debug.Log("objname = " + this.name + ", isLeftRight = " + isLeftRight + ", returnLR = " + returnLR);

        if(currentIndex != 1)
        {
            Debug.Log("텔포판별시작");
            bool temp = DontDestroy.thisIsPlayer.giveLeftRight();
            if(temp != isLeftRight)
            {
                Debug.Log(this.name + "는 맞음.");
                DontDestroy.thisIsPlayer.transform.position = new Vector3(this.transform.position.x +10, this.transform.position.y, DontDestroy.thisIsPlayer.transform.position.z);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isLeftRight) //enum순서상 다음 맵
        {
            targetIndex += 1;
        }
        else
        {
            targetIndex -= 1;
        }
        Debug.Log(targetIndex);
        Teleport(targetIndex);
    }

    public void Teleport(int MapNum)
    {
        DontDestroy.thisIsPlayer.getLeftRight(isLeftRight);
        SceneManager.LoadScene(MapNum);
    }

    public static bool GiveIsLR()
    {
        Debug.Log("텔레포터 호출, " + returnLR);
        return returnLR;
    }
}
