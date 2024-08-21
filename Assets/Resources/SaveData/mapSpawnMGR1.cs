using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mapSpawnMGR1 : MonoBehaviour
{
    private EnemySO enemySOData;
    private MapSO mapSOData;
    public List<GameObject> EnemySpawnList = new List<GameObject>();
    public List<GameObject> ObjectSpawnList = new List<GameObject>();

    public int MapNum;



    private void Awake()
    {
        enemySOData = Resources.Load<EnemySO>("SaveData/Enemy" + MapNum);
        mapSOData = Resources.Load<MapSO>("SaveData/mapSO" + MapNum);

        SpawnMethod();
        LoadObjectMethod();
    }

    public void SpawnMethod()
    {
        for (int i = 0; i < enemySOData.spawnList.Count; i++)
        {
            Debug.Log(enemySOData.spawnList.Count);
            Debug.Log(enemySOData.spawnList[i]);
            EnemySpawnList[i].GetComponent<SpawnPoint>().canSpawn = enemySOData.spawnList[i];
            EnemySpawnList[i].GetComponent<SpawnPoint>().SpawnObject();
        }
    }

    public void LoadObjectMethod()
    {
        for (int i = 0; i < mapSOData.objects.Count; i++)
        {
            ObjectSpawnList[i].GetComponent<SpawnPoint>().canSpawn = mapSOData.objects[i];
            ObjectSpawnList[i].GetComponent<SpawnPoint>().SpawnObject();

        }
    }

    public void RespawnEnemyMethod()
    {
        for (int i = 0; i < enemySOData.spawnList.Count; i++)
        {
            Debug.Log(enemySOData.spawnList.Count);
            Debug.Log(enemySOData.spawnList[i]);
            if (!EnemySpawnList[i].GetComponent<SpawnPoint>().canSpawn)
            {
                EnemySpawnList[i].GetComponent<SpawnPoint>().canSpawn = enemySOData.spawnList[i];
                EnemySpawnList[i].GetComponent<SpawnPoint>().SpawnObject();
            }

        }
    }
}
