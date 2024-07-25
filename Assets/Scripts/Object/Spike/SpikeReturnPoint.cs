using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeReturnPoint : MonoBehaviour
{
    GameObject spikeObj;
    Spike spike;

    private void Awake()
    {
        GameObject par = this.transform.parent.gameObject;

        List<Transform> gameObjects = new List<Transform>();
        foreach (Transform child in par.transform)
        {
            gameObjects.Add(child);
        }

        foreach (Transform obj in gameObjects)
        {
            if(obj.GetComponent<Spike>())
            {
                spikeObj = obj.gameObject;
            }
        }
        spike = spikeObj.GetComponent<Spike>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Define.PlayerTag))
        {
            Debug.Log(this.name + "이 " + this.transform + "값을 전해줌");
            spike.ReturnPos(this.transform);
        }
    }
    

}
