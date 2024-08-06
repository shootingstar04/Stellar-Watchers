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

    [SerializeField] private GameObject itemdata;

    //private float timer = 0f;
    //private const float StatusActive = 2f;


    private void Awake()
    {
        Player = GameObject.FindGameObjectWithTag(Define.PlayerTag);
        playermove = Player.GetComponent<PlayerMove>();

        if (cam == null)
        {
            Debug.Log("시도1");
            GameObject cam_temp = GameObject.Find("Virtual Camera");
            cam = cam_temp.GetComponent<cam_deadzone_test>();
        }

        if (cam == null)
        {
            Debug.Log("시도2");
        }

        playerdata = Resources.Load<PlayerData>("SaveData/PlayerSO");
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
        int lastSceneIndex = playerdata.SceneIndex;
        Debug.Log(lastSceneIndex);


        playerdata.Position = this.transform.position;
        Scene scene = SceneManager.GetActiveScene();
        playerdata.SceneIndex = scene.buildIndex;
        playerdata.Coin = ItemData.Instance.CurrentGold;
        Debug.Log(playerdata.Position + "위치, " + playerdata.Coin + "코인");

        if (scene.buildIndex != 2)
        {
            for (int i = 0; i < 4; i++)
            {
                MapSO mapso = Resources.Load<MapSO>("SaveData/MapSO" + i);
                Debug.Log(mapso);
                savedata.Objects.Add(new List<bool>());

                for (int j = 0; j < mapso.objects.Count; j++)
                {
                    savedata.Objects[i][j] = (mapso.objects[j]);
                }
            }

            /*
            if (scene.buildIndex > lastSceneIndex)
            {
                for (int i = (scene.buildIndex - 2); i <= (lastSceneIndex - 2); i++)
                {
                    MapSO mapso = Resources.Load<MapSO>("SaveData/MapSO" + i);

                    for (int j = 0; j < mapso.objects.Count; j++)
                    {
                        savedata.Objects[i][j].Add( mapso.objects[j]);
                    }
                }

            }
            else//씬 이하
            {
                for (int i = (scene.buildIndex - 2); i <= (lastSceneIndex - 2); i++)
                {
                    MapSO mapso = Resources.Load<MapSO>("SaveData/MapSO" + i);

                    for (int j = 0; j < mapso.objects.Count; j++)
                    {
                        savedata.Objects[i][j].Add( mapso.objects[j]);
                    }
                }
            }
            */

        }
        else
        {
            MapSO mapso = Resources.Load<MapSO>("SaveData/MapSO0");
            for (int i = 0; i < mapso.objects.Count; i++)
            {
                savedata.Objects[0][i] = (mapso.objects[i]);
            }
        }

        for (int i = 0; i < savedata.Objects.Count; i++)
        {
            for (int j = 0; j < savedata.Objects[i].Count; j++)
            {
                Debug.Log(savedata.Objects[i][j]);
            }
        }

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
        }
    }

}
