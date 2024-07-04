using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHealth : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //플레이어 체력 코드 추가
            Debug.Log("회복");
            Destroy(this.gameObject);
        }
    }
}
