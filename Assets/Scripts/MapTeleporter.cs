using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTeleporter : MonoBehaviour
{
    [SerializeField] int offset;
    [SerializeField] Transform TargetPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Define.PlayerTag))
        {
            Debug.Log("맵전환 작동");
            SceneManager.instance.NextMap(TargetPosition, offset);
        }
    }
}
