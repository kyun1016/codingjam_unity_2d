using UnityEngine;

[CreateAssetMenu(fileName = "New EffectData", menuName = "ScriptableObject/Effect Data")]
public class EffectData : ScriptableObject
{
    public float jumpHeight; // 1회 위로 순간이동
    public float dashLength; // 1회 앞으로 순간이동
    public float jumpSpeed; // 점프 속도
    public float maxJumpSpeed; // 최대 점프 속도
    public float speed; // 이동 속도
    public float maxSpeed; // 최대 이동 속도
    public bool block; // 장애물 피격판정 무시
    public float invulnerability; // 무적 시간
}