using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public bool canSpawn;
    public GameObject spawnObject;


    public void SpawnObject()
    {
        Instantiate(spawnObject, new Vector3(this.transform.position.x, this.transform.position.y, Vector3.zero.z), Quaternion.identity);
    }
}
