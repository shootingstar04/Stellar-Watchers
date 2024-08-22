using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StartBTN1 : MonoBehaviour
{
    private Button btnStart;

    [SerializeField] private GameObject continueCanvas;

    public static StartBTN1 instance;

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
        if (!PlayerPrefs.HasKey(Define.maxHp) || (PlayerPrefs.HasKey(Define.posX) && PlayerPrefs.GetFloat(Define.posX) + PlayerPrefs.GetFloat(Define.posY) + PlayerPrefs.GetFloat(Define.posZ) == 0))
        {
            Debug.Log("진행상황 없음.새로만듦");
            ResetData();

        }
        else
        {
            Debug.Log("진행상황 있음");
            continueCanvas.SetActive(true);
        }
    }
    public void ResetData()
    {
        SaveLoadManager1.instance.ResetPlayerData();
        SaveLoadManager1.instance.ResetMapData();

        SaveLoadManager1.instance.LoadMapData();
        SaveLoadManager1.instance.LoadPlayerData();
    }

    public void StartGame()
    {
        SaveLoadManager1.instance.LoadMapData();
        SaveLoadManager1.instance.LoadPlayerData();
    }
}
