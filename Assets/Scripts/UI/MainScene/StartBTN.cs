using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartBTN : MonoBehaviour
{
    private Button btnStart;

    private void Awake()
    {
        if(btnStart == null)
        {
            btnStart = GetComponent<Button>();
        }
    }

    private void Start()
    {
        btnStart.onClick.AddListener(() => startMap());
    }

    void startMap()
    {
        SceneManager.LoadScene("First0");
    }
}
