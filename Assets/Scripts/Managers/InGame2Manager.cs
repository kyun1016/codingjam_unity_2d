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
    public EnemyPoolManager _enemyPoolManager;
    public BackGround_InGame2[] _BackGroundTiles;

    public HUDDistance _HUDDistance;
    public HUDDistance2 _HUDDistance2;
    public HUDDistance3 _HUDDistance3;

    public HUD_ItemNode[] _HUDLongUseItems;
    public HUD_ItemNode[] _HUDUseItems;
    
    public void Initialize()
    {
        gameObject.SetActive(true);
        _HUD.gameObject.SetActive(true);
        _enemyPoolManager.gameObject.SetActive(true);
        _HUDDistance.Initialize();
        _HUDDistance2.Initialize();
        _HUDDistance3.Initialize();
        foreach (var tile in _BackGroundTiles)
        {
            tile.Initialization();
        }
        foreach (var item in _HUDLongUseItems)
        {
            item.Initialize();
        }
        foreach (var item in _HUDUseItems)
        {
            item.Initialize();
        }
        for (int i = 0; i < GameManager.instance._skillData.Count; i++)
        {
            if(i >= _HUDUseItems.Length)
            {
                break;
            }
            _HUDUseItems[i].SetItemData(GameManager.instance._skillData[i]);
            _HUDUseItems[i].SetCount(GameManager.instance._skillCount[i]);
        }
        for (int i = 0; i < GameManager.instance._permanentSkillData.Count; i++)
        {
            if(i >= _HUDLongUseItems.Length)
            {
                break;
            }
            _HUDLongUseItems[i].SetItemData(GameManager.instance._permanentSkillData[i]);
            _HUDLongUseItems[i].SetCount(GameManager.instance._permanentSkillCount[i]);
        }
        _isActive = true;
    }

    public void Reset()
    {
        _isActive = false;
        _HUD.gameObject.SetActive(false);
        _enemyPoolManager.gameObject.SetActive(false);
        gameObject.SetActive(false);
        _HUDDistance.Reset();
        _HUDDistance2.Reset();
        _HUDDistance3.Reset();
        foreach (var tile in _BackGroundTiles)
        {
            tile.Reset();
        }
        foreach (var item in _HUDLongUseItems)
        {
            item.Reset();
        }
        foreach (var item in _HUDUseItems)
        {
            item.Reset();
        }
    }
}