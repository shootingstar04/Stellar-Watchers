using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathScreen : MonoBehaviour
{
    public static DeathScreen Instance;

    [SerializeField] private GameObject canvas;

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
            canvas = GetComponentInChildren<GameObject>();
        }
    }

    public void ShowDeathScreen()
    {
        canvas.SetActive(true);
    }
}
