using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExistOne : MonoBehaviour
{
    public static GameObject instance;

    private void Awake()
    {
        Scene scene = SceneManager.GetActiveScene();
        Debug.Log(scene);
        if(scene.buildIndex == 0)
        {
            Destroy(this);
            return;
        }
        if (instance != null)
        {
            Debug.Log(this.name);
            Destroy(instance.gameObject);
        }

        instance = this.gameObject;

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
