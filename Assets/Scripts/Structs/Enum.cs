public class Enum
{
    public enum ItemCategory
    {
        special,
        speed,
        health,
        protection
    }
    public enum ItemEffect
    {
        jumpHeight, // 1회 위로 순간이동
        dashLength, // 1회 앞으로 순간이동
        jumpSpeed, // 점프 속도
        maxJumpSpeed, // 최대 점프 속도
        speed, // 이동 속도
        maxSpeed, // 최대 이동 속도
        block, // 장애물 피격판정 무시
        invulnerability // 무적 시간
    }

    public enum Language
    {
        English,
        Korean,
        Japanese,
        Chinese,
        Spanish
    }
}
