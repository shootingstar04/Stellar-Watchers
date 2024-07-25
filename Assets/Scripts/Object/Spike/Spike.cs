using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Spike : MonoBehaviour
{
    [SerializeField] bool isSpike;
    private static Transform priviousPos;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Define.PlayerTag))
        {
            if(isSpike)
            {
                Debug.Log("µ¥¹ÌÁö");
                StartCoroutine(PlayHitEffect(collision.gameObject));
            }    
        }
        ReturnPlayer(collision.gameObject);
    }

    IEnumerator PlayHitEffect(GameObject obj)
    {
        SpriteRenderer sprite = obj.GetComponent<SpriteRenderer>();
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.white;
    }

    private void ReturnPlayer(GameObject player)
    {
        player.transform.position = new Vector3(priviousPos.position.x, priviousPos.position.y, player.transform.position.z);
    }

    public static void ReturnPos(Transform pos)
    {
        priviousPos = pos;
    }

}
