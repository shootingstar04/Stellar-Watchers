using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class toTitleButton : MonoBehaviour
{
    Button me;

    private void Awake()
    {
        me = GetComponent<Button>();
    }

    private void Start()
    {
        me.onClick.AddListener(() => SceneManager.LoadScene(0));
    }
}
