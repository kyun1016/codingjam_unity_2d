using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New EnemyData", menuName = "ScriptableObject/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public int ID;
    public float Damage;
    public float[] spawnPositions;
    // public Sprite sprite; 스프라이트 별 폴리곤을 잡는 것을 목적으로 하기에 Sprite 관리는 제거함
}