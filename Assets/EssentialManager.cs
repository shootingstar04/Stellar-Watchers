using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EssentialManager : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject sceneManager;
    [SerializeField] private GameObject ui;
    [SerializeField] private GameObject deathScreen;
    [SerializeField] private GameObject particleManager;
    [SerializeField] private GameObject coinPool;
    [SerializeField] private GameObject mainCamera;
    [SerializeField] private GameObject virtualCamera;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag(Define.PlayerTag))
        {
            Debug.Log("�� ����. " + this.gameObject.name + " ����");
            Destroy(this.gameObject);
        }
        else
        {
            CreateEssentials();
            SaveLoadManager.instance.LoadPlayerData();
            changeConfinder.instance.ChangeConf();
        }
    }

    void CreateEssentials()
    {
        Instantiate(sceneManager);
        Instantiate(ui);
        Instantiate(player);
        Instantiate(deathScreen);
        Instantiate(particleManager);
        Instantiate(coinPool);
        Instantiate(mainCamera);
        Instantiate(virtualCamera);
    }
}
