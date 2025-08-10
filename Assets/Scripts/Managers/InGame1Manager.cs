using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InGame1Manager : MonoBehaviour
{
    [Header("Control")]
    public bool _isActive;
    [Header("HUD")]
    public HUDTimer _HUDTimer;
    public HUD_InventoryPool _HUDInventoryPool;             // 해당 경로 아래에 _selectedItems가 실제 선택된 아이템 List
    public HUD_BackGround_InGame1 _HUDBackGround;
    public List<ItemData> _ItemDatas;
    public List<HUDInventoryNode> _HUDInventoryNodes;
    public bool[] _baseEnablePosition = new bool[4 * 8];
    public int _lastHandleRootItemNodeIndex;
    public Dictionary<int, int> _selectedItemDatas = new Dictionary<int, int>();

    // private void Awake()
    // {
    //     // _HUDTimer = GetComponent<HUDTimer>();
    //     // _HUDInventoryPool = GetComponent<HUD_InventoryPool>();
    //     // _HUDBackGround = GetComponent<HUD_BackGround_InGame1>();
    //     _HUDInventoryNodes = new List<HUDInventoryNode>(GetComponentsInChildren<HUDInventoryNode>());
    // }
    public void Initialize()
    {
        gameObject.SetActive(true);
        _HUDTimer.Initialize();
        _HUDInventoryPool.Initialize();
        _HUDBackGround.Initialize();
        foreach (var node in _HUDInventoryNodes)
        {
            node.Initialize();
        }
        _isActive = true;
    }

    public void Reset()
    {
        _selectedItemDatas.Clear();
        GameManager.instance._skillCooldown.Clear();
        GameManager.instance._skillEffect.Clear();
        GameManager.instance._skillData.Clear();
        GameManager.instance._permanentSkillCooldown.Clear();
        GameManager.instance._permanentSkillEffect.Clear();
        GameManager.instance._permanentSkillData.Clear();
        foreach (var data in _HUDInventoryPool._selectedItems)
        {
            if (_selectedItemDatas.ContainsKey(data._ItemData.itemID))
                _selectedItemDatas[data._ItemData.itemID]++;
            else
                _selectedItemDatas.Add(data._ItemData.itemID, 1);
        }
        foreach (var kvp in _selectedItemDatas)
        {
            int itemID = kvp.Key;
            int count = kvp.Value;
            ItemData data = _ItemDatas[itemID];
            if (data.trigger)
            {
                GameManager.instance._skillCount.Add(count);
                GameManager.instance._skillCooldown.Add(0.0f);
                GameManager.instance._skillEffect.Add(false);
                GameManager.instance._skillData.Add(data);
            }
            else
            {
                GameManager.instance._permanentSkillCount.Add(count);
                GameManager.instance._permanentSkillCooldown.Add(0.0f);
                GameManager.instance._permanentSkillEffect.Add(true);
                GameManager.instance._permanentSkillData.Add(data);
            }
        }
        _HUDTimer.Reset();
        _HUDInventoryPool.Reset();
        _HUDBackGround.Reset();
        _isActive = false;
        gameObject.SetActive(false);
    }
}