using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeReturnPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Define.PlayerTag))
        {
            Debug.Log(this.name + "�� " + this.transform + "���� ������");
            Spike.ReturnPos(this.transform);
        }
    }
}
