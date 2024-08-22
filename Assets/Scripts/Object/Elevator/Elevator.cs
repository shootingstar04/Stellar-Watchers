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

    private float moveTime = 4f; //�̵��üӽð�
    private Vector3 floorPos = Vector3.zero;

    public bool isWorking = false;  

    //Ÿ�� �۵�
    public void Active()
    {
        isWorking = true;                       //���������Ͱ� ���� ������(���������� ���� ���� ����ġ �۵�)
        guard.gameObject.SetActive(true);       //���������� �۵��� �÷��̾ �������� �ʰ� �����ִ� ���� ������Ʈ Ȱ��
        Vector3 pos = Vector3.zero;             //��ǥ��ġ �ʱ�ȭ

        if(Mathf.Approximately(cage.transform.position.y, SwitchDown.transform.position.y))                             //������������ ��ġ�� �Ʒ��� ����ġ�� ����ϴٸ�
        {
            pos = new Vector3(cage.transform.position.x, SwitchUp.transform.position.y, cage.transform.position.z);     //��ǥ��ġ�� ���� ����ġ�� ����
        }
        else if (Mathf.Approximately(cage.transform.position.y, SwitchUp.transform.position.y))                         //������������ ��ġ�� ���� ����ġ�� ����ϴٸ�
        {
            pos = new Vector3(cage.transform.position.x, SwitchDown.transform.position.y, cage.transform.position.z);   //��ǥ��ġ�� �Ʒ��� ����ġ�� ����
        }
        else
        {
            Debug.Log("����");                    //����ó��
            return;
        }
        cage.transform.DOMove(pos, moveTime).OnComplete(() => EndFunction());   //DOTween �̿�. moveTime���� pos��ġ�� �̵�. �Ϸ�� EndFunction ȣ��.
    }
    protected void EndFunction()
    {
        isWorking = false;                  //���������Ͱ� ���� ���������� ����(���������� ���� ���� ����ġ �۵�) 
        guard.gameObject.SetActive(false);  //���� ������Ʈ ��Ȱ��ȭ
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
