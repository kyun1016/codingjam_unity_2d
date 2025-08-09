using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InGame2Manager : MonoBehaviour
{
    [Header("HUD")]
    [SerializeField] private HUDTimer _HUDTimer;
    
    public void Initialize()
    {
        _HUDTimer.Initialize();
    }

    public void Reset()
    {
        _HUDTimer.Reset();
    }
}