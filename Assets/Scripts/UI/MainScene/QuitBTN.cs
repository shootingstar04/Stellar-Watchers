using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuitBTN : MonoBehaviour
{
    private Button quitBtn;

    private void Awake()
    {
        if (quitBtn == null)
        {
            quitBtn = GetComponent<Button>();
        }
    }
    void Start()
    {
        quitBtn.onClick.AddListener(() => quitGame());
    }

    void quitGame()
    {
        Application.Quit();
    }

}
