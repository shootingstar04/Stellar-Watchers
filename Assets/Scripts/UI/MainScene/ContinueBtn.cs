using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueBtn : MonoBehaviour
{
    Button btnContinue;

    private void Awake()
    {
        btnContinue = GetComponent<Button>();
    }

    private void Start()
    {
        btnContinue.onClick.AddListener(() => StartBTN.instance.StartGame());
    }
}
