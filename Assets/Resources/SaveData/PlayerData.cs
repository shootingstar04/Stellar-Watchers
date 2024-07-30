using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "PlayerSO", menuName = "PlayerSO", order = 9)]
public class PlayerData : ScriptableObject
{
    [Header("General")]
    public Vector3 Position;
    public int SceneIndex;
    public int Coin;
    public int MaxHp;
    public int MaxSp;
    public int CurrentSp;

    [Header("ItemData")]
    public List<bool> Acquired_ItemData = new List<bool>();

    [Header("SkillData")]
    public List<bool> Acquired_SkillData = new List<bool>(9); //0~8까지 존재

    public SkillSet.skill[,] settedSkill = new SkillSet.skill[2, 2]
    {
        { SkillSet.skill.none, SkillSet.skill.none },
        { SkillSet.skill.none, SkillSet.skill.none }
    };


    [Header("ProgressData")]
    public List<bool> Aquired_ProgressData = new List<bool>(7); //0~6까지 존재

    public int stoneCount = 0;
    public int pointCount = 0;
    public int reinforcementCount = 0;
}
