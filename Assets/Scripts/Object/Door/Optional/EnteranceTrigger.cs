using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnteranceTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.CompareTag(Define.PlayerTag))
        {
            BossDoor.bossdoorClose();
        }
    }
}
