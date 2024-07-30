using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraDontDestroy : MonoBehaviour
{
    public cameraDontDestroy instance;
    private static bool hasInstance = false;

    private void Awake()
    {
        if (hasInstance)
        {
            Destroy(gameObject);
        }
        else
        {
            hasInstance = true;
            DontDestroyOnLoad(gameObject);
        }

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
