using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InGame1Manager : MonoBehaviour
{
    [Header("HUD")]
    public HUDTimer _HUDTimer;
    public HUD_InventoryPool _HUDInventoryPool;
    public HUD_BackGround_InGame1 _HUDBackGround;
    public List<ItemData> _ItemDatas;

    // private void Awake()
    // {
    //     _HUDTimer = GetComponent<HUDTimer>();
    //     _HUDInventoryPool = GetComponent<HUD_InventoryPool>();
    //     _HUDBackGround = GetComponent<HUD_BackGround_InGame1>();
    // }
    public void Initialize()
    {
        _HUDTimer.Initialize();
        _HUDInventoryPool.Initialize();
        _HUDBackGround.Initialize();
    }

    public void Reset()
    {
        _HUDTimer.Reset();
        _HUDInventoryPool.Reset();
        _HUDBackGround.Reset();
    }
}