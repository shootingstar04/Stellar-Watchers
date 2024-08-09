using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public void Scene_Load(string sceneName) 
    {
        GameObject player = GameObject.FindGameObjectWithTag(Define.PlayerTag);
        Destroy(player);
        SceneManager.LoadScene(sceneName);
    }
}
