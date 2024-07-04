using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InspectorManager : MonoBehaviour
{
    private List<GameObject> info = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        info.Add(GameObject.Find("Stat"));
        info.Add(GameObject.Find("Item"));
        info.Add(GameObject.Find("Constellation"));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void set_pos()
    {
           
    }
}
