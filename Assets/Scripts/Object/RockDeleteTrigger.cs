using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDeleteTrigger : MonoBehaviour
{
    /*
    private bool isTriggered = false;
    [SerializeField] private LayerMask layer;
    [SerializeField] private GameObject target;


    private void Update()
    {
        isTriggered = Physics2D.OverlapBox(this.transform.position, new Vector2(2, 10), 0f, layer) ;
        if(isTriggered)
        {
            Destroy(target) ;
            Destroy(this.gameObject) ;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(this.transform.position, new Vector2(2, 10));

    }
    */

 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag(Define.HazardTag))
        {
            Debug.Log("»ç¶óÁü");
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }

}
