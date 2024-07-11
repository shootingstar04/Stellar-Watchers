using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D cage;
    [SerializeField] private Collider2D guard;
    [SerializeField] private GameObject TargetPosition;

    public static Elevator EV;

    private Vector2 CurrentPos;
    private Vector2 TargetPos;

    private float timer = 0f;
    private float moveTime = 3f; //�̵��üӽð�

    private float t;

    protected short isUpDown = 1; //-1 ����, 1 ��
    public bool isWorking = false;

    private void Awake()
    {
        EV = this;
        TargetPosition = GameObject.Find("TargetPosition");
        TargetPosition.transform.position = TargetPos;
        CurrentPos = this.transform.position;
    }

    //Ÿ�� �۵�
    public void Active()
    {
        timer = 0f;
        isWorking= true;
        guard.gameObject.SetActive(true);
    }

    private void Update()
    {
        if(isWorking)
        {
            timer += Time.deltaTime;
            t = timer / moveTime;
            t = t * t*(3f - 2f * t);

            Vector2 newPosition = Vector2.Lerp(CurrentPos, TargetPos, t);
            cage.MovePosition(newPosition);
            if(cage.transform.position.y == TargetPos.y)
            {
                EndFunction();
            }
        }
    }

    protected void EndFunction()
    {
        isWorking = false;
        guard.gameObject.SetActive(false);
        isUpDown = (short)(isUpDown * -1);
        TargetPos = CurrentPos;
        CurrentPos = this.transform.position;
    }

    

}
