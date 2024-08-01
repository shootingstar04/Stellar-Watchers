using System.Collections;
using System.Collections.Generic;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Interactable : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject UI;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera Cam;
    private PlayerMove playermove;
    private cam_deadzone_test cam;

    bool inside = false;
    [SerializeField] bool isSave;

    [SerializeField] private Canvas canvas;

    private PlayerData playerdata;

    [SerializeField] private GameObject itemdata;

    //private float timer = 0f;
    //private const float StatusActive = 2f;


    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag(Define.PlayerTag);
        playermove = Player.GetComponent<PlayerMove>();
        cam = Cam.GetComponent<cam_deadzone_test>();

        playerdata = Resources.Load<PlayerData>("SaveData/PlayerSO");
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

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (isSave)
            {
                SaveMethod();
            }
            else
            {
                RockMethod();
            }

        }
        /*
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            cam.interact = true;
            timer = 0f;
        }

        if(Input.GetKey(KeyCode.UpArrow))
        {

            timer += Time.deltaTime;
        }


        if (timer >= StatusActive)
        {
            //StatusMethod();
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            if(timer < StatusActive)
            {
                SaveMethod();
            }
            else
            {
                //EndStatusMethod();
            }
            cam.interact = false;
        }
        */

    }

    void SaveMethod()
    {
        playerdata.Position = this.transform.position;
        UnityEngine.SceneManagement.Scene scene = SceneManager.GetActiveScene();
        playerdata.SceneIndex = scene.buildIndex;
        playerdata.Coin = itemdata.GetComponent<ItemData>().CurrentGold;
        Debug.Log(playerdata.Position + "위치, " + playerdata.Coin + "코인");
        
        TextPopUp.instance.show_PopUp("저장");
    }

    void RockMethod()
    {
        ReinforcePopUp.instance.show_PopUp();
    }


    /*
    void StatusMethod()
    {

        Debug.Log("강화 작동");

        Player.GetComponent<PlayerMove>().PauseMove();
        cam.isInCutScene = true;
        canvas.gameObject.SetActive(true);
    }
    
    void EndStatusMethod()
    {
        timer = 0f;
        Player.GetComponent<PlayerMove>().RestartMove();
        cam.isInCutScene = false;
        canvas.gameObject.SetActive(false);
    }


    public void Activated()
    { //활성화는 홀드. 토글 아님
        if (Input.GetKey(KeyCode.UpArrow))
        {
            Player.GetComponent<PlayerMove>().PauseMove();
            
            //playermove.moveSpeed = 0;
            playermove.jumpForce = 0;
            playermove.dashForce = 0f;
            playermove.dashDuration = 0f;
            playermove.invincibilityDuration = 0f;
            playermove.maxJumpHeight = 0f;
            //playermove.maxJumpTime = 0f;

            cam.isInCutScene = true;
            canvas.gameObject.SetActive(true);

        }
        else
        {
            Player.GetComponent<PlayerMove>().RestartMove();
            
            //playermove.moveSpeed = 7;
            playermove.jumpForce = 18;
            playermove.dashForce = 20f;
            playermove.dashDuration = 0.2f;
            playermove.invincibilityDuration = 1f;
            playermove.maxJumpHeight = 3f;
           // playermove.maxJumpTime = 0.4f;
            
            cam.isInCutScene = false;
            canvas.gameObject.SetActive(false);
        }

    }
    */
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Define.PlayerTag))
        {
            inside = true;
            cam.interact = true;
            UI.SetActive(inside);

            //timer = 0f;
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
            /*
            playermove.moveSpeed = 7;
            playermove.jumpForce = 18;
            playermove.dashForce = 20f;
            playermove.dashDuration = 0.2f;
            playermove.invincibilityDuration = 1f;
            playermove.maxJumpHeight = 3f;
            playermove.maxJumpTime = 0.4f;
            */
            cam.isInCutScene = false;
            canvas.gameObject.SetActive(false);

        }
    }

}
