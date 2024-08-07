using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartBTN : MonoBehaviour
{
    private Button btnStart;

    private SaveData savedata;
    private PlayerData playerdata;

    [SerializeField] private GameObject continueCanvas;

    public static StartBTN instance;

    private void Awake()
    {
        instance = this;

        if (btnStart == null)
        {
            btnStart = GetComponent<Button>();
        }

        savedata = Resources.Load<SaveData>("SaveData/SaveData");
        playerdata = Resources.Load<PlayerData>("SaveData/PlayerSO");
    }

    private void Start()
    {
        btnStart.onClick.AddListener(() => startMap());
    }

    void startMap()
    {
        if (savedata.Objects.Count == 0)
        {
            ResetGame();
        }
        else
        {
            continueCanvas.SetActive(true);
        }

    }

    public void ResetGame()
    {
        playerdata.Coin = 0;
        playerdata.SceneIndex = 2;
        playerdata.MaxHp = 5;
        playerdata.MaxSp = 5;
        playerdata.CurrentSp = 5;
        playerdata.Position = new Vector3(0, 0, 0);
        SceneManager.LoadScene("First0");
    }

    public void ContinueGame()
    {
        SaveLoadManager.instance.LoadData();
    }
}
