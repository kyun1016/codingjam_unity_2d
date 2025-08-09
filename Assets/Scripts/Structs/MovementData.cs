using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MovementData", menuName = "ScriptableObject/Movement Data")]
public class MovementData : ScriptableObject
{
    public float acceleration;
    public float baseSpeed;
    public float maxSpeed;
    public float jumpEffect;
    public float dashEffect;
}