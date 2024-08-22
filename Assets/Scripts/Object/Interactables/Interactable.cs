using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Unity.VisualScripting;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Interactable : MonoBehaviour
{
    [SerializeField] private GameObject Player;
    [SerializeField] private GameObject UI;

    private PlayerMove playermove;
    private cam_deadzone_test cam;

    bool inside = false;
    [SerializeField] bool isSave;

    private PlayerData playerdata;
    private SaveData savedata;

    //private float timer = 0f;
    //private const float StatusActive = 2f;


    private void Awake()
    {
        /*

        if(Player == null)
        {
            Debug.Log(this.gameObject.name + "플레이어 못받음");
        }

        if (cam == null)
        {
            Debug.Log("시도1");
            GameObject cam_temp = GameObject.Find("Virtual Camera");
            cam = cam_temp.GetComponent<cam_deadzone_test>();
        }
        */

        //playerdata = Resources.Load<PlayerData>("SaveData/PlayerSO");
        savedata = Resources.Load<SaveData>("SaveData/SaveData");
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
        Debug.Log("저장중");
        Resurrect();
        PlayerHealth.instance.modify_HP(99);
        //mapSpawnMGR spawnmanager = GameObject.Find("SpawnMGR").GetComponent<mapSpawnMGR>();
        //spawnmanager.SaveMethod();

        SaveLoadManager1.instance.SavePlayerData(this.transform);
        SaveLoadManager1.instance.SaveMapData();

        TextPopUp.instance.show_PopUp("저장");

        //mapSpawnMGR.instance.RespawnEnemyMethod();

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
        if(Player == null)
        {
            Player = GameObject.FindGameObjectWithTag(Define.PlayerTag);
            playermove = Player.GetComponent<PlayerMove>();
        }

        if (cam == null)
        {
            Debug.Log("시도1");
            GameObject cam_temp = GameObject.FindObjectOfType<CinemachineVirtualCamera>().gameObject;
            cam = cam_temp.GetComponent<cam_deadzone_test>();
        }
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
        }
    }


    public void Resurrect()
    {
        Debug.Log("몬스터부활");
        for (int i = 0; i < 4; i++)
        {
            MapSO mapso = Resources.Load<MapSO>("SaveData/MapSO" + i);
            for(int j = 0; j<mapso.objects.Count; j++)
            {
                mapso.objects[j] = true;
            }
        }
    }
}
