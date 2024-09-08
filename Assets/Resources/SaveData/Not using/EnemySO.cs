using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "Enemy(Map num)", menuName = "EnemySO", order = 8)]
public class EnemySO : ScriptableObject
{
    public List<bool> spawnList = new List<bool>();
}
