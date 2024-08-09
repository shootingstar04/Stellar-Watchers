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
    [Tooltip("오른쪽이 -1, 왼쪽이 1. 텔레포트 시 offset과 offsetSize를 곱해 스폰 오프셋 조절")]
    [SerializeField] int offset;
    [SerializeField] int offsetSize = 2;

    [SerializeField] GameObject mapSpawnMGR;

    private PlayerData playerdata;

    private void Awake()
    {

        playerdata = Resources.Load<PlayerData>("SaveData/PlayerSO");
        if (DontDestroy.thisIsPlayer.isTeleported && DontDestroy.thisIsPlayer.numbering == Numbering)
        {

            SceneTransition.instance.FadeIn();
            DontDestroy.thisIsPlayer.transform.position = new Vector3(this.transform.position.x + (offsetSize * offset), this.transform.position.y, DontDestroy.thisIsPlayer.transform.position.z);
            restartmove();

        }

        Scene scene = SceneManager.GetActiveScene();

        if(playerdata.Reseted && scene.buildIndex == 2)
        {
            DontDestroy.thisIsPlayer.transform.position = playerdata.Position;
        }

        Debug.Log(ItemData.Instance.CurrentGold + " 원");
    }

    private void Start()
    {
        if (changeConfinder.instance != null)
        {
            changeConfinder.instance.ChangeConf();
        }
        else
        {
            Debug.Log("없음");
        }

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
                Debug.Log("맵스폰매니저존재함 저장시작");
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
