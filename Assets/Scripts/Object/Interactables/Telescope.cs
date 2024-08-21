using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        Player = GameObject.FindGameObjectWithTag(Define.PlayerTag);
        playermove = Player.GetComponent<PlayerMove>();

        if (Cam == null)
        {
            GameObject cam_temp = (GameObject)FindObjectOfType(typeof(CinemachineVirtualCamera));
            Debug.Log(cam_temp);
            cam = cam_temp.GetComponent<cam_deadzone_test>();
        }
        else
        {
            cam = Cam.GetComponent<cam_deadzone_test>();
        }


    }

    private void Update()
    {
        if (inside)
        {
            Active();
        }
    }


    public void Active()
    {

        if (Input.GetKey(KeyCode.UpArrow))
        {
            Player.GetComponent<PlayerMove>().PauseMove();
            cam.isInCutScene = true;
            canvas.gameObject.SetActive(true);

        }
        else
        {
            Player.GetComponent<PlayerMove>().RestartMove();
            cam.isInCutScene = false;
            canvas.gameObject.SetActive(false);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Define.PlayerTag))
        {
            inside = true;
            cam.interact = true;
            UI.SetActive(inside);

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(Define.PlayerTag))
        {
            inside = false;
            cam.interact = false;
            UI.SetActive(inside);

            Player.GetComponent<PlayerMove>().RestartMove();
            cam.isInCutScene = false;
            canvas.gameObject.SetActive(false);

        }
    }

}

