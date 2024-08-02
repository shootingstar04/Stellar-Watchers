using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Search;
using UnityEngine;

public class changeConfinder : MonoBehaviour
{

    private GameObject obj;
    private PolygonCollider2D mapEdge;
    public static CinemachineConfiner2D conf;

    public static changeConfinder instance;

    private void Awake()
    {
        instance = this;
        conf = GetComponent<CinemachineConfiner2D>();
    }

    public void ChangeConf()
    {
        obj = GameObject.FindGameObjectWithTag("MapEdge");
        Debug.Log(obj);
        mapEdge = obj.GetComponent<PolygonCollider2D>();

        if (mapEdge != null)
        {
            CameraBorder(mapEdge);
        }



    }

    public static void CameraBorder(PolygonCollider2D newConfiner)
    {
        conf.m_BoundingShape2D = newConfiner;
    }

}
