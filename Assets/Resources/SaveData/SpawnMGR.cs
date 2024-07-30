using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnMGR : MonoBehaviour
{

    private void Awake()
    {
        //LoadMap();

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            SaveMap();
        }
        if(Input.GetKeyDown(KeyCode.Alpha5))
        {
            LoadMap();
        }
    }


    void LoadMap()
    {
        string map;
        map = SceneManager.GetActiveScene().buildIndex.ToString();

        Debug.Log(map);
        string filePath = Application.persistentDataPath + "/" + "map" + map + ".json";
        string fromJsonData = File.ReadAllText(filePath);
        Debug.Log(fromJsonData);
        MapJson mapJson = JsonUtility.FromJson<MapJson>(fromJsonData);
        Debug.Log(mapJson);
    }

    public void SaveMap()
    {
        MapJson mapJson = new MapJson();
        List<bool> list = new List<bool> { true, true, true, true, true };
        mapJson.SpawnList = list;
        string mapData = SceneManager.GetActiveScene().buildIndex.ToString() + "map";
        string map3 = JsonUtility.ToJson(mapJson);
    }
}
