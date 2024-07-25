using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeReturnPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Define.PlayerTag))
        {
            Debug.Log(this.name + "이 " + this.transform + "값을 전해줌");
            Spike.ReturnPos(this.transform);
        }
    }
}
