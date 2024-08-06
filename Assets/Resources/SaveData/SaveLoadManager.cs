using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour
{
    private SaveData savedata;
    private PlayerData playerdata;

    public static SaveLoadManager instance;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }


    public void LoadData()
    {
        savedata = Resources.Load<SaveData>("SaveData/SaveData");

        for (int i = 0; i < 4; i++)
        {
            MapSO mapso = Resources.Load<MapSO>("SaveData/mapSO" + i);
            {
                for (int j = 0; j < savedata.Objects[i].Count; j++)
                {
                    mapso.objects[j] = savedata.Objects[i][j];
                }
            }
        }


        playerdata = Resources.Load<PlayerData>("SaveData/PlayerSO");

        DontDestroy.thisIsPlayer.isTeleported = false;
        SceneManager.LoadScene(playerdata.SceneIndex);
        DontDestroy.thisIsPlayer.transform.position = new Vector3(playerdata.Position.x, playerdata.Position.y, playerdata.Position.z);
        int gold = ItemData.Instance.CurrentGold - playerdata.Coin;
        ItemData.Instance.modify_gold(-gold);

        PlayerHealth.instance.modify_HP(5);

        SceneTransition.instance.FadeIn();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            LoadData();
        }
    }
}
