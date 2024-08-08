using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResetBTN : MonoBehaviour
{
    Button btn;

    private void Awake()
    {
        btn = GetComponent<Button>();
    }

    private void Start()
    {
        btn.onClick.AddListener(() => resetData());
    }

    void resetData()
    {
        if(PlayerPrefs.HasKey(Define.maxHp))
        {
            PlayerPrefs.DeleteAll();
        }
        else
        {
            Debug.Log("이미 데이터 없음");
            return;
        }
    }
}
