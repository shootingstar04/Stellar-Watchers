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
    [SerializeField] private GameObject SwitchUp;
    [SerializeField] private GameObject SwitchDown;

    private Vector2 UpDownPosition;

    public static Elevator EV;

    private float moveTime = 3f; //이동시속시간
    private Vector3 floorPos = Vector3.zero;

    public bool isWorking = false;

    private void Awake()
    {
        EV = this;

    }

    //타면 작동
    public void Active()
    {
        isWorking = true;
        guard.gameObject.SetActive(true);
        Vector3 pos = Vector3.zero;

        if(Mathf.Approximately(cage.transform.position.y, SwitchDown.transform.position.y))
        {
            pos = new Vector3(cage.transform.position.x, SwitchUp.transform.position.y, cage.transform.position.z);
        }
        else if (Mathf.Approximately(cage.transform.position.y, SwitchUp.transform.position.y))
        {
            pos = new Vector3(cage.transform.position.x, SwitchDown.transform.position.y, cage.transform.position.z);
        }
        else
        {
            Debug.Log("오류");
            return;
        }
        cage.transform.DOMove(pos, moveTime).OnComplete(() => EndFunction());
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

    protected void EndFunction()
    {
        isWorking = false;
        guard.gameObject.SetActive(false);
    }



}
