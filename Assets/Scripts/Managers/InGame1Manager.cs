using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InGame1Manager : MonoBehaviour
{
    [Header("HUD")]
    [SerializeField] private HUDTimer _HUDTimer;
    [SerializeField] private HUD_InventoryPool _HUDInventoryPool;
    public 
    public void Initialize()
    {
        _HUDTimer.Initialize();
        _HUDInventoryPool.Initialize();
    }

    public void Reset()
    {
        _HUDTimer.Reset();
        _HUDInventoryPool.Reset();
    }
}