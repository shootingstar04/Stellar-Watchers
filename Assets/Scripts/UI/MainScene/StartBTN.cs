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
        if (PlayerPrefs.GetInt(Define.sceneIndex) != 2 || PlayerPrefs.HasKey(Define.maxHp))
        {
            continueCanvas.SetActive(true);
        }
        else
        {
            ResetData();
        }
    }

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
            SceneManager.LoadScene(2);
        }
        else
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt(Define.sceneIndex));
        }
    }


}
