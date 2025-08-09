using UnityEngine;

[CreateAssetMenu(fileName = "New ItemData", menuName = "ScriptableObject/Item Data")]
public class ItemData : ScriptableObject
{
    public Enum.ItemCategory category; // 아이템 카테고리
    public Enum.ItemEffect effectType;
    public int itemID;
    public string[] itemName;
    [TextArea]
    public string[] description;
    public Sprite[] icon;
    public float effectValue;
    public float effectDuration; // 효과 지속 시간
    public Vector2Int[] gridPosition; // (0,0), (1,-1) 같은 논리적 좌표
    public float dropRate;
}