using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExistOne : MonoBehaviour
{
    public static GameObject instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log(this.name);
            Destroy(instance.gameObject);
        }

        instance = this.gameObject;

        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
