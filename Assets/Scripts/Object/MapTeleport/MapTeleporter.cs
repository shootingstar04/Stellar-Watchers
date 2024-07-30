using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapTeleporter : MonoBehaviour
{
    [Header("텔레포트 시 제일 중요한 부분")]
    [Header("목표 맵의 텔레포터와 똑같은 번호여야 함. 0은 쓰지 말것")]
    [Tooltip("씬 로드되면 해당 씬의 텔레포드 Numbering중 똑같은 곳에 플레이어 옮겨줄 생각")]
    [SerializeField] int Numbering;
    [Header("목표 씬의 이름과 똑같아야함")]
    [Tooltip("이름 받아서 LoadScene해줄 생각")]
    [SerializeField] string TargetMapName;
    [Header("해당 텔레포터 위치가 오른쪽인지 왼쪽인지. 왼쪽이 1")]
    [Tooltip("오른쪽이 -1, 왼쪽이 1. 텔레포트 시 스폰 오프셋 조절")]
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
