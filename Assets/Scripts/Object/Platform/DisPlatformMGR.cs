using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DisPlatform;

public class DisPlatformMGR : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    private Collider2D col;


    [Header("ÇÃ·§Æû µîÀå Áö¿¬½Ã°£")]
    [SerializeField] private float ReappearTime = 2f;


    private void Awake()
    {
        col = GetComponent<Collider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag(Define.PlayerTag))
        {
            callInvoke();
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(Define.PlayerTag))
        {
            CancelInvoke();
        }    
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(Define.PlayerTag))
        {
            callInvoke();
        }
    }

    public void callInvoke()
    {
        Invoke("callTimer", ReappearTime);
    }

    void callTimer()
    {
        col.isTrigger = !col.isTrigger;
        platform.SetActive(!platform.activeInHierarchy);
    }

}
