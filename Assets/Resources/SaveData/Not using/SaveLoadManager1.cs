using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveLoadManager1 : MonoBehaviour
{
    public static SaveLoadManager1 instance;

    private void Awake()
    {
        instance = this;
    }

    public void SavePlayerData(Transform trans)
    {
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
        Debug.Log("SLMGR1에서 작동");
        SceneManager.LoadScene(1);

        DontDestroy.thisIsPlayer.transform.position = new Vector3(PlayerPrefs.GetFloat(Define.posX), PlayerPrefs.GetFloat(Define.posY), PlayerPrefs.GetFloat(Define.posZ));
        ItemData.Instance.set_gold(PlayerPrefs.GetInt(Define.coins));
        PlayerSP.instance.set_max_SP(PlayerPrefs.GetInt(Define.maxSp));
        PlayerHealth.instance.set_max_HP(PlayerPrefs.GetInt(Define.maxHp));
        PlayerSP.instance.set_SP(PlayerPrefs.GetInt(Define.curSp));
    }


    public void SaveMapData()
    {
        for (int index = 0; index < 6; index++)
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
        for (int index = 0; index < 6; index++)
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
        for (int index = 0; index < 6; index++)
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
