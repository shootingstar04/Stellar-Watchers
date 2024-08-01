using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mapSpawnMGR : MonoBehaviour
{
    private EnemySO enemySOData;
    public mapSpawnMGR instance;
    public List<GameObject> EnemySpawnList = new List<GameObject>();

    private void Awake()
    {
        instance = this;
        enemySOData = Resources.Load<EnemySO>("SaveData/Enemy" + SceneManager.GetActiveScene().buildIndex.ToString());

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
    }

    public void SaveMethod()
    {
        Debug.Log("저장시작");
        for(int i = 0; i < EnemySpawnList.Count; i++)
        {
            enemySOData.spawnList[i] = EnemySpawnList[i].GetComponent<SpawnPoint>().canSpawn;
        }
    }

    
}
