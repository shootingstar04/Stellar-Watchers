using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mapSpawnMGR : MonoBehaviour
{
    private MapSO mapdata;
    public mapSpawnMGR instance;
    public List<GameObject> spawnList = new List<GameObject>();

    private void Awake()
    {
        instance = this;
        mapdata = Resources.Load<MapSO>("SaveData/Map" + SceneManager.GetActiveScene().buildIndex.ToString());

        LoadMethod();
    }

    void LoadMethod()
    {
        for(int i = 0; i < mapdata.spawnList.Count; i++)
        {
            if (mapdata.spawnList[i])
            {
                spawnList[i].GetComponent<SpawnPoint>().SpawnObject();
            }
        }
    }

    public void SaveMethod()
    {
        
    }
}
