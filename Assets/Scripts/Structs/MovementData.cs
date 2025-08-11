using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New MovementData", menuName = "ScriptableObject/Movement Data")]
public class MovementData : ScriptableObject
{
    public Enum.MovementType movementType; // Enum for different movement types
    public float acceleration;
    public float baseSpeed;
    public float maxSpeed;
    public float jumpEffect;
    public float dashEffect;
}