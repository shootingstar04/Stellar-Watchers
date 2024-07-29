using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;
using TMPro;

public class Spike : MonoBehaviour
{
    [SerializeField] bool isSpike;
    private Transform priviousPos;

    [SerializeField] private int Damage = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(Define.PlayerTag))
        {
            collision.GetComponent<PlayerMove>().PauseMove();
            if (isSpike)
            {
                Debug.Log("데미지");
                PlayerHealth.instance.modify_HP(-Damage);
                StartCoroutine(PlayHitEffect(collision.gameObject));
            }
        }
        
    }

    IEnumerator PlayHitEffect(GameObject obj)
    {
        SpriteRenderer sprite = obj.GetComponent<SpriteRenderer>();
        sprite.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.white;
        SceneTransition.instance.FadeOut();
        yield return new WaitForSeconds(0.5f);
        SceneTransition.instance.FadeIn();
        ReturnPlayer(obj.gameObject);
    }

    private void ReturnPlayer(GameObject player)
    {
        if (priviousPos != null)
        {
            player.transform.position = new Vector3(priviousPos.position.x, priviousPos.position.y, player.transform.position.z);
        }
        else //예외처리(priviousPos못받음)
        {
            GameObject par = this.transform.parent.gameObject;
            List <Transform> gameObjects = new List<Transform>();
            foreach(Transform child in par.transform)
            {
                gameObjects.Add(child);
            }
            float tempVal = 999f;
            Transform temp = null;
            foreach (Transform obj in gameObjects)
            {
                if (!obj.GetComponent<Spike>())
                {
                    float absVal = Mathf.Abs(obj.position.x - player.transform.position.x);
                    if(absVal < tempVal)
                    {
                        tempVal = absVal;
                        temp = obj;
                    }
                }
                Debug.Log("temp는 " + temp);
            }
            player.transform.position = new Vector3(temp.position.x, temp.position.y, player.transform.position.z);
        }
        
        Debug.Log(player.name);
        PlayerMove pMove = player.GetComponent<PlayerMove>();
        if (pMove == null)
            Debug.Log("fuckyou");

        pMove.RestartMove();
    }

    public void ReturnPos(Transform pos)
    {
        priviousPos = pos;
    }

}
