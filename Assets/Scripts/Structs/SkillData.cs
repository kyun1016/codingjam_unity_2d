using UnityEngine;
[CreateAssetMenu(fileName = "New SkillData", menuName = "ScriptableObject/Skill Data")]
public class SkillData : ScriptableObject
{
    public int skillID;
    public string skillName;
    [TextArea]
    public string description;
    public Sprite icon;
    public Vector2Int gridPosition; // (0,0), (1,-1) 같은 논리적 좌표
}