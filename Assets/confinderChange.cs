using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class confinderChange : MonoBehaviour
{
    public static confinderChange instance;
    public CinemachineConfiner2D conf;


    private void Awake()
    {
        instance = this;
    }

    public void ConfinderChange(int targetMapNum)
    {
        GameObject[] mapEdges = GameObject.FindGameObjectsWithTag("MapEdge");

        foreach(GameObject mapEgde in mapEdges)
        { 
            if(mapEgde.GetComponent<MapEdge>().MapNum == targetMapNum && !mapEgde.GetComponent<MapEdge>().isSecret)
            {
                Debug.Log(mapEgde.name + " °¡ mapedge");
                conf.m_BoundingShape2D = mapEgde.GetComponent<PolygonCollider2D>();
            }
        }
    }
    public void ConfinderChangeSecret(int targetMapNum)
    {
        GameObject[] mapEdges = GameObject.FindGameObjectsWithTag("MapEdge");

        foreach (GameObject mapEgde in mapEdges)
        {
            if (mapEgde.GetComponent<MapEdge>().MapNum == targetMapNum && mapEgde.GetComponent<MapEdge>().isSecret)
            {
                conf.m_BoundingShape2D = mapEgde.GetComponent<PolygonCollider2D>();
            }
        }
    }
}
