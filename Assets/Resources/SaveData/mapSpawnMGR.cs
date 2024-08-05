using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mapSpawnMGR : MonoBehaviour
{
    private EnemySO enemySOData;
    private MapSO mapSOData;
    public mapSpawnMGR instance;
    public List<GameObject> EnemySpawnList = new List<GameObject>();
    public List<GameObject> ObjectSpawnList = new List<GameObject>();

    private Scene currentScene;

    private void Awake()
    {
        instance = this;
        currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "First0")
        {
            enemySOData = Resources.Load<EnemySO>("SaveData/Enemy" + 0);
            mapSOData = Resources.Load<MapSO>("SaveData/mapSO" + 0);
        }
        else
        {
            enemySOData = Resources.Load<EnemySO>("SaveData/Enemy" + (SceneManager.GetActiveScene().buildIndex - 2));
            mapSOData = Resources.Load<MapSO>("SaveData/mapSO" + (SceneManager.GetActiveScene().buildIndex - 2));
        }

        Debug.Log(mapSOData);

        LoadMethod();
    }

    public void LoadMethod()
    {
        for (int i = 0; i < enemySOData.spawnList.Count; i++)
        {
            if (enemySOData.spawnList[i])
            {
                EnemySpawnList[i].GetComponent<SpawnPoint>().SpawnObject();
            }
        }
        /*
        if(mapSOData == null)
        {
            return;
        }*/
        for (int i = 0; i < mapSOData.objects.Count; i++)
        {
            if (mapSOData.objects[i])
            {
                ObjectSpawnList[i].GetComponent<SpawnPoint>().SpawnObject();
            }
        }
    }

    public void SaveMethod()
    {
        Debug.Log("저장시작");
        for (int i = 0; i < EnemySpawnList.Count; i++)
        {
            enemySOData.spawnList[i] = EnemySpawnList[i].GetComponent<SpawnPoint>().canSpawn;
        }

        if (mapSOData == null)
        {
            return;
        }
        for (int i = 0; i < ObjectSpawnList.Count; i++)
        {
            mapSOData.objects[i] = ObjectSpawnList[i].GetComponent<SpawnPoint>().canSpawn;
        }
    }


}
