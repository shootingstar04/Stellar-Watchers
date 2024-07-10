using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
using UnityEngine;
using UnityEngine.UI;

using static UnityEditor.Experimental.GraphView.GraphView;

public class Telescope : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject UI;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera Cam;
    private PlayerMove playermove;
    private cam_deadzone_test cam;

    bool inside = false;

    [SerializeField] private Canvas canvas;


    private void Awake()
    {
        playermove = Player.GetComponent<PlayerMove>();
        cam = Cam.GetComponent<cam_deadzone_test>();

    }

    private void Update()
    {
        if (inside)
        {
            Activated();
        }
    }

    public void Activated()
    { //È°¼ºÈ­´Â È¦µå. Åä±Û ¾Æ´Ô
        if (Input.GetKey(KeyCode.UpArrow))
        {
            playermove.moveSpeed = 0;
            playermove.jumpForce = 0;
            playermove.dashForce = 0f;
            playermove.dashDuration = 0f;
            playermove.invincibilityDuration = 0f;
            playermove.maxJumpHeight = 0f;
            playermove.maxJumpTime = 0f;
            cam.isInCutScene = true;
            canvas.gameObject.SetActive(true);

        }
        else
        {
            playermove.moveSpeed = 7;
            playermove.jumpForce = 18;
            playermove.dashForce = 20f;
            playermove.dashDuration = 0.2f;
            playermove.invincibilityDuration = 1f;
            playermove.maxJumpHeight = 3f;
            playermove.maxJumpTime = 0.4f;
            cam.isInCutScene = false;
            canvas.gameObject.SetActive(false);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(Define.PlayerTag))
        {
            inside = true;
            UI.SetActive(inside);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag(Define.PlayerTag))
        {
            inside = false;
            UI.SetActive(inside);
        }
    }

}
