using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathScreen : MonoBehaviour
{
    public static DeathScreen Instance;

    [SerializeField] private GameObject canvas;

    private bool iamActive = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }

        if (canvas == null)
        {
            canvas = transform.GetChild(0).gameObject;
        }
    }

    public void ShowDeathScreen()
    {
        canvas.SetActive(true);
        iamActive = true;
    }

    private void Update()
    {
        if(!iamActive)
        {
            return;
        }
        if(Input.anyKeyDown)
        {
            iamActive = false;

            GameObject player = GameObject.FindGameObjectWithTag(Define.PlayerTag);
            player.GetComponent<PlayerMove>().respawn();
            canvas.SetActive(false);
        }
    }
}
