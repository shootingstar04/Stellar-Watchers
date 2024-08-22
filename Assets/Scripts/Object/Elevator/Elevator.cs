using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;
using System.ComponentModel;

public class Elevator : MonoBehaviour
{
    [SerializeField] public Rigidbody2D cage;
    [SerializeField] private Collider2D guard;
    [SerializeField] public GameObject SwitchUp;
    [SerializeField] public GameObject SwitchDown;

    private GameObject player;

    private Vector2 UpDownPosition;

    private float moveTime = 4f; //이동시속시간
    private Vector3 floorPos = Vector3.zero;

    public bool isWorking = false;  

    //타면 작동
    public void Active()
    {
        isWorking = true;                       //엘리베이터가 현재 운행중(운행중이지 않을 때만 스위치 작동)
        guard.gameObject.SetActive(true);       //엘리베이터 작동시 플레이어가 떨어지지 않게 막아주는 가드 오브젝트 활성
        Vector3 pos = Vector3.zero;             //목표위치 초기화

        if(Mathf.Approximately(cage.transform.position.y, SwitchDown.transform.position.y))                             //엘리베이터의 위치가 아래층 스위치랑 비슷하다면
        {
            pos = new Vector3(cage.transform.position.x, SwitchUp.transform.position.y, cage.transform.position.z);     //목표위치를 위층 스위치로 변경
        }
        else if (Mathf.Approximately(cage.transform.position.y, SwitchUp.transform.position.y))                         //엘리베이터의 위치가 위층 스위치랑 비슷하다면
        {
            pos = new Vector3(cage.transform.position.x, SwitchDown.transform.position.y, cage.transform.position.z);   //목표위치를 아래층 스위치로 변경
        }
        else
        {
            Debug.Log("오류");                    //예외처리
            return;
        }
        cage.transform.DOMove(pos, moveTime).OnComplete(() => EndFunction());   //DOTween 이용. moveTime동안 pos위치로 이동. 완료시 EndFunction 호출.
    }
    protected void EndFunction()
    {
        isWorking = false;                  //엘리베이터가 현재 운행중이지 않음(운행중이지 않을 때만 스위치 작동) 
        guard.gameObject.SetActive(false);  //가드 오브젝트 비활성화
    }


    /*
    private void Update()
    {
        if (isWorking)
        {
            timer += Time.deltaTime;
            t = timer / moveTime;
            t = t * t * (3f - 2f * t);

            float newPosition = Mathf.Lerp(CurrentPos.y, TargetPos.y, t);
            cage.MovePosition(new Vector2(this.transform.position.x, isUpDown * newPosition));
            if (cage.transform.position.y == TargetPos.y)
            {
                EndFunction();
            }
        }
    }
    */
}
