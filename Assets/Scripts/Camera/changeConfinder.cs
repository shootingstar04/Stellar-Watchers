using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeConfinder : MonoBehaviour
{

    private GameObject obj;
    [SerializeField] private PolygonCollider2D mapEdge;
    public static CinemachineConfiner2D conf;

    public static changeConfinder instance;

    private void Start()
    {
        instance = this;
        conf = GetComponent<CinemachineConfiner2D>();

        CameraBorder(null);

        obj = GameObject.FindGameObjectWithTag("MapEdge");
        Debug.Log(obj);
        mapEdge = obj.GetComponent<PolygonCollider2D>();

        ChangeConf();
    }

    public void ChangeConf()
    {
        if (mapEdge == null)
        {
            obj = GameObject.FindGameObjectWithTag("MapEdge");
            Debug.Log(obj);
            mapEdge = obj.GetComponent<PolygonCollider2D>();
        }
        CameraBorder(mapEdge);
    }

    public static void CameraBorder(PolygonCollider2D newConfiner)
    {
        conf.m_BoundingShape2D = newConfiner;
    }

}
