using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mapSpawnMGR : MonoBehaviour
{
    private MapSO mapdata;
    public mapSpawnMGR instance;

    private void Awake()
    {
        instance = this;
        mapdata = Resources.Load<MapSO>("SaveData/Map" + SceneManager.GetActiveScene().buildIndex.ToString());

        LoadMethod();
    }

    void LoadMethod()
    {

    }

    public void SaveMethod()
    {
        
    }
}
