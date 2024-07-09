using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Telescope : MonoBehaviour
{
    private GameObject objectInTrigger = new GameObject();

    private static Vector2 circleCenter;
    private static float circleRadius = 2f;

    private PlayerMove playerMove;

    [SerializeField] private Canvas canvas;


    private void Awake()
    {

        circleCenter = this.transform.position;
    }

    private void Update()
    {
        LayerMask layer = LayerMask.GetMask(Define.PlayerTag);
        Collider2D collider = Physics2D.OverlapCircle(circleCenter, circleRadius,layer);
        objectInTrigger = collider.GetComponent<GameObject>();
        if(objectInTrigger.gameObject.CompareTag(Define.PlayerTag))
        {
            playerMove = objectInTrigger.gameObject.GetComponent<PlayerMove>();
            Activated();
        }
    }


    public void Activated()
    { //È°¼ºÈ­´Â È¦µå. Åä±Û ¾Æ´Ô
        if (Input.GetKey(KeyCode.UpArrow))
        {
            playerMove.moveSpeed = 0;
            playerMove.jumpForce = 0;
            canvas.gameObject.SetActive(true);

        }
        else;
        {
            playerMove.moveSpeed = 7;
            playerMove.jumpForce = 18;
            canvas.gameObject.SetActive(false);
        }

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(circleCenter, circleRadius);

    }


}
