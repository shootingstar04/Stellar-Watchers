using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "SaveData",menuName = "savedata", order = 1)]
public class SaveData : ScriptableObject
{
    public List<List<bool>> Objects = new List<List<bool>>();
}
