using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartBTN : MonoBehaviour
{
    private Button btnStart;

    [SerializeField] private GameObject continueCanvas;

    public static StartBTN instance;

    private void Awake()
    {
        instance = this;

        if (btnStart == null)
        {
            btnStart = GetComponent<Button>();
        }

    }

    private void Start()
    {
        btnStart.onClick.AddListener(() => preStep());
    }

    void preStep()
    {
        if (PlayerPrefs.HasKey(Define.maxHp))
        {
            Debug.Log("진행상황 있음");
            continueCanvas.SetActive(true);
        }
        else
        {
            Debug.Log("진행상황 없음.새로만듦");
            ResetData();
        }
    }
    public void ResetData()
    {
        SaveLoadManager.instance.ResetPlayerData();
        SaveLoadManager.instance.ResetMapData();

        SaveLoadManager.instance.LoadMapData();
        SaveLoadManager.instance.LoadPlayerData();
    }

    public void StartGame()
    {
        if (PlayerPrefs.GetInt(Define.sceneIndex) == 1)
        {
            PlayerPrefs.SetInt(Define.sceneIndex, 2);
        }

        SaveLoadManager.instance.LoadMapData();
        SaveLoadManager.instance.LoadPlayerData();

    }

    /*
    public void ResetData()
    {
        SaveLoadManager.instance.ResetPlayerData();
        SaveLoadManager.instance.ResetMapData();
        StartGame();
    }

    public void StartGame()
    {

        if (GameObject.Find("Player"))
        {
            if (PlayerPrefs.GetInt(Define.sceneIndex) == 1)
            {
                PlayerPrefs.SetInt(Define.sceneIndex, 2);
            }

            SaveLoadManager.instance.LoadMapData();
            SaveLoadManager.instance.LoadPlayerData();
        }
        else
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt(Define.sceneIndex));
        }
    }
    */


}
