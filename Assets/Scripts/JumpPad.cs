using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class JumpPad : MonoBehaviour
{
    private float BoostPower = 20.0f;
    private Rigidbody2D rigid;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag(Define.PlayerTag))
        {
            rigid = collision.gameObject.GetComponent<Rigidbody2D>();
            rigid.AddForce(Vector2.up * BoostPower, ForceMode2D.Impulse);
        }
    }
}
