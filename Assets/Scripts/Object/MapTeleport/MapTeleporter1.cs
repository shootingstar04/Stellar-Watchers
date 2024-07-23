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

    [Header("��ǥ �ڷ���Ʈ ��ġ")]
    [Tooltip("�� ���� ���� ���� ���̸� true, ���� ���̸� fasle")]
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
        Debug.Log(currentIndex + "�� ���� buildindex");

        returnLR = isLeftRight;

        Debug.Log("objname = " + this.name + ", isLeftRight = " + isLeftRight + ", returnLR = " + returnLR);

        if(currentIndex != 1)
        {
            Debug.Log("�����Ǻ�����");
            bool temp = DontDestroy.thisIsPlayer.giveLeftRight();
            if(temp != isLeftRight)
            {
                Debug.Log(this.name + "�� ����.");
                DontDestroy.thisIsPlayer.transform.position = new Vector3(this.transform.position.x +10, this.transform.position.y, DontDestroy.thisIsPlayer.transform.position.z);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isLeftRight) //enum������ ���� ��
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
        Debug.Log("�ڷ����� ȣ��, " + returnLR);
        return returnLR;
    }
}
