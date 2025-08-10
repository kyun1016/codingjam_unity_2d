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
    public HUD_InventoryPool _HUDInventoryPool;
    public HUD_BackGround_InGame1 _HUDBackGround;
    public List<ItemData> _ItemDatas;
    public List<HUDInventoryNode> _HUDInventoryNodes;
    public bool[] _baseEnablePosition = new bool[4 * 8];
    public int _lastHandleRootItemNodeIndex;

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
        _HUDTimer.Reset();
        _HUDInventoryPool.Reset();
        _HUDBackGround.Reset();
        _isActive = false;
        gameObject.SetActive(false);
    }
}