using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "mapSO (mapIndex)", menuName = "mapSO", order = 0)]
public class MapSO : ScriptableObject
{
    public List<bool> objects = new List<bool>();
}
