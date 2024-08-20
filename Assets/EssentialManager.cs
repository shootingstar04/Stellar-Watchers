using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using System.Security;
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

    [SerializeField] private GameObject saveLoadManager;

    [SerializeField] private GameObject impulseManager;

    private void Start()
    {

        if (GameObject.FindGameObjectWithTag(Define.PlayerTag))
        {

        }
        else
        {
            Instantiate(player);
        }

        if (GameObject.FindObjectOfType(typeof(Camera)))
        {

        }
        else
        {
            Instantiate(mainCamera);
        }

        if (GameObject.FindObjectOfType(typeof(CinemachineVirtualCamera)))
        {

        }
        else
        {
            Instantiate(virtualCamera);
        }

        if (GameObject.FindObjectOfType(typeof(SaveLoadManager)))
        {

        }
        else
        {
            Instantiate(saveLoadManager);
        }

        if (GameObject.FindObjectOfType(typeof(ExistOne)))
        {

        }
        else
        {
            Instantiate(ui);
        }

        if (GameObject.FindObjectOfType(typeof(SceneTransition)))
        {

        }
        else
        {
            Instantiate(sceneManager);
        }

        if (GameObject.FindObjectOfType(typeof(DeathScreen)))
        {

        }
        else
        {
            Instantiate(deathScreen);
        }

        if (GameObject.FindObjectOfType(typeof(ParticleManager)))
        {

        }
        else
        {
            Instantiate(particleManager);
        }

        if (GameObject.FindObjectOfType(typeof(CoinPool)))
        {

        }
        else
        {
            Instantiate(coinPool);
        }

        if (GameObject.FindGameObjectWithTag(Define.PlayerTag) && GameObject.FindObjectOfType(typeof(SaveLoadManager)) && PlayerPrefs.HasKey("isLoadedProperly"))
        {
            if (PlayerPrefs.GetInt("isLoadedProperly") == 1)
            {
                SaveLoadManager.instance.LoadPlayerData();
            }
        }

        if (GameObject.FindGameObjectWithTag(Define.PlayerTag) && GameObject.FindObjectOfType(typeof(CinemachineVirtualCamera)))
        {
            changeConfinder.instance.ChangeConf();
        }

        Debug.Log("다 있음. " + this.gameObject.name + " 삭제");
        Destroy(this.gameObject);

    }

}
