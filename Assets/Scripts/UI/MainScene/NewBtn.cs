using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBtn : MonoBehaviour
{
    Button btnNew;

    private void Awake()
    {
        btnNew = GetComponent<Button>();
    }

    private void Start()
    {
        btnNew.onClick.AddListener(() => StartBTN.instance.ResetData());
    }
}
