using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] protected Rigidbody2D cage;
    [SerializeField] private Collider2D guard;
    [SerializeField] private GameObject SwitchUp;
    [SerializeField] private GameObject SwitchDown;

    public static Elevator EV;

    public Vector2 CurrentPos = new Vector2(0, 0);
    private Vector2 TargetPos = new Vector2(0, 0);

    private float timer = 0f;
    private float moveTime = 3f; //이동시속시간

    private float t;

    protected short isUpDown = 1; //-1 위층, 1 땅
    public bool isWorking = false;

    private void Awake()
    {
        EV = this;
        CurrentPos.y = SwitchDown.transform.position.y;
        TargetPos.y = SwitchUp.transform.position.y;

    }

    //타면 작동
    public void Active()
    {
        timer = 0f;
        isWorking = true;
        guard.gameObject.SetActive(true);
    }

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

    protected void EndFunction()
    {
        isWorking = false;
        guard.gameObject.SetActive(false);
        isUpDown = (short)(isUpDown * -1);

        Vector2 temp = TargetPos;
        TargetPos = CurrentPos;
        CurrentPos = TargetPos;
    }



}
