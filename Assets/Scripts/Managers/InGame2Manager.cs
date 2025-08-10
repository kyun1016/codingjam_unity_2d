using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InGame2Manager : MonoBehaviour
{
    [Header("Parameter")]
    public bool _isActive = false;
    [Header("HUD")]
    public GameObject _HUD;
    public EnemyPool _enemyPool;

    public HUDDistance _HUDDistance;
    public HUDDistance2 _HUDDistance2;
    public HUDDistance3 _HUDDistance3;
    
    public void Initialize()
    {
        gameObject.SetActive(true);
        _HUD.gameObject.SetActive(true);
        _enemyPool.gameObject.SetActive(true);
        _HUDDistance.Initialize();
        _HUDDistance2.Initialize();
        _HUDDistance3.Initialize();
        _isActive = true;
    }

    public void Reset()
    {
        _isActive = false;
        _HUD.gameObject.SetActive(false);
        _enemyPool.gameObject.SetActive(false);
        gameObject.SetActive(false);
        _HUDDistance.Reset();
        _HUDDistance2.Reset();
        _HUDDistance3.Reset();
    }
}