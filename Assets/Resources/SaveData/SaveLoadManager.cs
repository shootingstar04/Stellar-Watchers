using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
//using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadManager : MonoBehaviour
{
    public static SaveLoadManager instance;

    private void Awake()
    {
        instance = this;
    }

    public void SavePlayerData(Transform trans)
    {
        PlayerPrefs.SetInt(Define.sceneIndex, SceneManager.GetActiveScene().buildIndex);

        PlayerPrefs.SetFloat(Define.posX, trans.position.x);
        PlayerPrefs.SetFloat(Define.posY, trans.position.y);
        PlayerPrefs.SetFloat(Define.posZ, trans.position.z);

        PlayerPrefs.SetInt(Define.coins, ItemData.Instance.CurrentGold);
        PlayerPrefs.SetInt(Define.maxSp, PlayerSP.instance.MaxSP);
        PlayerPrefs.SetInt(Define.maxHp, PlayerHealth.instance.MaxHP);
        PlayerPrefs.SetInt(Define.curSp, PlayerSP.instance.CurSP);

    }

    public void ResetPlayerData()
    {
        PlayerPrefs.SetInt(Define.sceneIndex, 1);

        PlayerPrefs.SetFloat(Define.posX, 0);
        PlayerPrefs.SetFloat(Define.posY, 0);
        PlayerPrefs.SetFloat(Define.posZ, 0);

        PlayerPrefs.SetInt(Define.coins, 0);
        PlayerPrefs.SetInt(Define.maxSp, 5);
        PlayerPrefs.SetInt(Define.maxHp, 5);
        PlayerPrefs.SetInt(Define.curSp, 5);
    }

    public void LoadPlayerData()
    {
        DontDestroy.thisIsPlayer.isTeleported = false;
        SceneManager.LoadScene(PlayerPrefs.GetInt(Define.sceneIndex));
        DontDestroy.thisIsPlayer.transform.position = new Vector3(PlayerPrefs.GetFloat(Define.posX), PlayerPrefs.GetFloat(Define.posY), PlayerPrefs.GetFloat(Define.posZ));
        ItemData.Instance.modify_gold(PlayerPrefs.GetInt(Define.coins));
        PlayerSP.instance.set_max_SP(PlayerPrefs.GetInt(Define.maxSp));
        PlayerHealth.instance.set_max_HP(PlayerPrefs.GetInt(Define.maxHp));
        PlayerSP.instance.set_SP(PlayerPrefs.GetInt(Define.curSp));
    }

    public void LoadNewPlayerData()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt(Define.sceneIndex));
        DontDestroy.thisIsPlayer.transform.position = new Vector3(PlayerPrefs.GetFloat(Define.posX), PlayerPrefs.GetFloat(Define.posY), PlayerPrefs.GetFloat(Define.posZ));
        ItemData.Instance.modify_gold(PlayerPrefs.GetInt(Define.coins));
        PlayerSP.instance.set_max_SP(PlayerPrefs.GetInt(Define.maxSp));
        PlayerHealth.instance.set_max_HP(PlayerPrefs.GetInt(Define.maxHp));
        PlayerSP.instance.set_SP(PlayerPrefs.GetInt(Define.curSp));
    }


    public void SaveMapData()
    {
        for (int index = 0; index < 4; index++)
        {
            MapSO mapso = Resources.Load<MapSO>("SaveData/mapSO" + index);
            for (int objects = 0; objects < mapso.objects.Count; objects++)
            {
                if (mapso.objects[objects])
                {
                    PlayerPrefs.SetInt("Map" + index + "." + objects, 1);
                }
                else
                {
                    PlayerPrefs.SetInt("Map" + index + "." + objects, 0);
                }
            }
        }
    }

    public void ResetMapData()
    {
        for (int index = 0; index < 4; index++)
        {
            MapSO mapso = Resources.Load<MapSO>("SaveData/mapSO" + index);
            for (int objects = 0; objects < mapso.objects.Count; objects++)
            {
                PlayerPrefs.SetInt("Map" + index + "." + objects, 1);
            }
        }
    }

    public void LoadMapData()
    {
        for (int index = 0; index < 4; index++)
        {
            MapSO mapso = Resources.Load<MapSO>("SaveData/mapSO" + index);
            for (int objects = 0; objects < mapso.objects.Count; objects++)
            {
                if (PlayerPrefs.GetInt("Map" + index + "." + objects) == 1)
                {
                    mapso.objects[objects] = true;
                }
                else
                {
                    mapso.objects[objects] = false;
                }
            }
        }
    }
}
