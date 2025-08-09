using UnityEngine;
[CreateAssetMenu(fileName = "New ItemData", menuName = "ScriptableObject/Item Data")]
public class ItemData : ScriptableObject
{
    public int itemID;
    public string itemName;
    [TextArea]
    public string description;
    public Sprite icon;
    public Vector2Int[] gridPosition; // (0,0), (1,-1) 같은 논리적 좌표
}