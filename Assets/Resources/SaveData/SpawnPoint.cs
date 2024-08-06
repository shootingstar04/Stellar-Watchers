using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public bool canSpawn;
    public GameObject spawnObject;

    public void SpawnObject()
    {
        if (canSpawn)
        {
            Debug.Log("canSpawn�̶� " + spawnObject.name +"������Ŵ");
            Instantiate(spawnObject, new Vector3(this.transform.position.x, this.transform.position.y, Vector3.zero.z), Quaternion.identity, this.transform);
        }
        else
        {
            Debug.Log("cannotspawn�̶� " + spawnObject.name + "��������");
        }

    }
}
