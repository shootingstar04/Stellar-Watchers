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
            Debug.Log("canSpawn이라 " + spawnObject.name +"스폰시킴");
            Instantiate(spawnObject, new Vector3(this.transform.position.x, this.transform.position.y, Vector3.zero.z), Quaternion.identity, this.transform);
        }
        else
        {
            Debug.Log("cannotspawn이라 " + spawnObject.name + "스폰안함");
        }

    }
}
