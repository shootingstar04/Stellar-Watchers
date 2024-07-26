using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraDontDestroy : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
