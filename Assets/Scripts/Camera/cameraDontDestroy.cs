using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraDontDestroy : MonoBehaviour
{
    public cameraDontDestroy instance;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        Camera camera = GetComponent<Camera>();
        Camera[] cameras = FindObjectsOfType<Camera>();
        if(cameras.Length > 1)
        {
            Debug.Log("2∞≥¿ÃªÛ");
            Destroy(camera);
        }
    }
}
