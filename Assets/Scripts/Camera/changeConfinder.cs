using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changeConfinder : MonoBehaviour
{

    private GameObject[] objs;
    private GameObject obj;
    private GameObject Secret;
    [SerializeField] private PolygonCollider2D mapEdge;
    public static CinemachineConfiner2D conf;

    public static changeConfinder instance;

    private void Start()
    {
        instance = this;
        conf = GetComponent<CinemachineConfiner2D>();

        CameraBorder(null);

        FindObj();

        ChangeConf();
    }

    public void ChangeConf()
    {
        if (mapEdge == null)
        {
            FindObj();
        }
        CameraBorder(mapEdge);
    }

    public static void CameraBorder(PolygonCollider2D newConfiner)
    {
        conf.m_BoundingShape2D = newConfiner;
    }

    public void ChangeConfAsSecret()
    {
        if(Secret == null)
        {
            return;
        }
        CameraBorder(Secret.GetComponent<PolygonCollider2D>());
    }

    public void FindObj()
    {
        objs = GameObject.FindGameObjectsWithTag("MapEdge");
        Debug.Log(objs);
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].name == "MapEdge")
            {
                obj = objs[i];
            }
            else
            {
                Secret = objs[i];
            }
        }
        mapEdge = obj.GetComponent<PolygonCollider2D>();
    }

}
