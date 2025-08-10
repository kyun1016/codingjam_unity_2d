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
    
    public void Initialize()
    {
        gameObject.SetActive(true);
        _HUD.gameObject.SetActive(true);
        _enemyPool.gameObject.SetActive(true);
        _isActive = true;
    }

    public void Reset()
    {
        _isActive = false;
        _HUD.gameObject.SetActive(false);
        _enemyPool.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}