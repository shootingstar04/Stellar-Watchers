using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Map num", menuName = "MapSO", order = 8)]
public class MapSO : ScriptableObject
{
    public List<bool> SpawnList = new List<bool>();
}
