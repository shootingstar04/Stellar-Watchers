using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSOindex : MonoBehaviour
{
    SpawnPoint spawnpoint;

    private void Start()
    {
        spawnpoint = GetComponentInParent<SpawnPoint>();
    }

    public void returnBool()
    {
        spawnpoint.canSpawn = false;
    }
}
