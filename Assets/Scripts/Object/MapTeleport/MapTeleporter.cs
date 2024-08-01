using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapTeleporter : MonoBehaviour
{
    [Header("�ڷ���Ʈ �� ���� �߿��� �κ�")]
    [Header("��ǥ ���� �ڷ����Ϳ� �Ȱ��� ��ȣ���� ��. 0�� ���� ����")]
    [Tooltip("�� �ε�Ǹ� �ش� ���� �ڷ����� Numbering�� �Ȱ��� ���� �÷��̾� �Ű��� ����")]
    [SerializeField] int Numbering;
    [Header("��ǥ ���� �̸��� �Ȱ��ƾ���")]
    [Tooltip("�̸� �޾Ƽ� LoadScene���� ����")]
    [SerializeField] string TargetMapName;
    [Header("�ش� �ڷ����� ��ġ�� ���������� ��������. ������ 1")]
    [Tooltip("�������� -1, ������ 1. �ڷ���Ʈ �� offset�� offsetSize�� ���� ���� ������ ����")]
    [SerializeField] int offset;
    [SerializeField] int offsetSize = 2;

    [SerializeField] GameObject mapSpawnMGR;

    private void Awake()
    {
        if (DontDestroy.thisIsPlayer.isTeleported && DontDestroy.thisIsPlayer.numbering == Numbering)
        {
            SceneTransition.instance.FadeIn();
            DontDestroy.thisIsPlayer.transform.position = new Vector3(this.transform.position.x + (offsetSize * offset), this.transform.position.y, DontDestroy.thisIsPlayer.transform.position.z);
            restartmove();

        }
    }

    private void Start()
    {
        if (mapSpawnMGR == null)
        {
            mapSpawnMGR = GameObject.Find("SpawnMGR");
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Define.PlayerTag))
        {
            if (mapSpawnMGR != null)
            {
                Debug.Log("�ʽ����Ŵ��������� �������");
                mapSpawnMGR.GetComponent<mapSpawnMGR>().SaveMethod();
            }

            DontDestroy.thisIsPlayer.GetNumbering(Numbering, true);
            StartCoroutine(Transition(TargetMapName));
            collision.GetComponent<PlayerMove>().PauseMove();
        }
    }
    IEnumerator Transition(string MapName)
    {
        SceneTransition.instance.FadeOut();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(MapName);
    }

    void restartmove()
    {
        GameObject player = DontDestroy.thisIsPlayer.gameObject;
        player.GetComponent<PlayerMove>().RestartMove();
    }
}
