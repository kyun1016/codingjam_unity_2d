using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New EnemyData", menuName = "ScriptableObject/Enemy Data")]
public class EnemyData : ScriptableObject
{
    [Header("Basic Info")]
    public int ID;
    public float Damage;
    [Header("Position")]
    public float[] spawnLocation;
    [Header("Movement")]
    public Enum.MovementType movementType;
    public float scale;
    public float jumpScale;
    public float diffSpeed;
}