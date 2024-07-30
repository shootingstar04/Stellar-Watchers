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
    [Tooltip("�������� -1, ������ 1. �ڷ���Ʈ �� ���� ������ ����")]
    [SerializeField] int offset;

    private void Awake()
    {
        if(DontDestroy.thisIsPlayer.isTeleported && DontDestroy.thisIsPlayer.numbering == Numbering)
        {
            SceneTransition.instance.FadeIn();
            DontDestroy.thisIsPlayer.transform.position = new Vector3(this.transform.position.x + (10 * offset), this.transform.position.y, DontDestroy.thisIsPlayer.transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Define.PlayerTag))
        {
            DontDestroy.thisIsPlayer.GetNumbering(Numbering, true);
            StartCoroutine(Transition(TargetMapName));
        }
    }
    IEnumerator Transition(string MapName)
    {
        SceneTransition.instance.FadeOut();
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(MapName);
    }
}
