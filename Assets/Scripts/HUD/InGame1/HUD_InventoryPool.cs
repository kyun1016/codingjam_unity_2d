using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // 이벤트 시스템 사용을 위해 필수!
using UnityEngine.UI;           // CanvasGroup을 사용하기 위해 필요
using UnityEngine.InputSystem;

public class HUD_InventoryPool : MonoBehaviour
{
    public bool[] _enablePosition = new bool[4 * 8];
    public HUD_InventoryItem _itemPrefab;
    public List<HUD_InventoryItem> _items;
    public List<SimpleObjectPool<HUD_InventoryItem>> _itemPools;
    public GameObject _inventory;
    public List<HUD_InventoryItem> _selectedItems;

    void Awake()
    {
        _itemPools = new List<SimpleObjectPool<HUD_InventoryItem>>();
        _items = new List<HUD_InventoryItem>();
        DevLog.Log($"Item Data Count: {GameManager.instance._InGame1Manager._ItemDatas.Count}"); // Debug log for item data count
        for (int i = 0; i < GameManager.instance._InGame1Manager._ItemDatas.Count; i++)
        {
            HUD_InventoryItem item = Instantiate(_itemPrefab, transform);
            item.name = $"item_{i}";
            item.Initialize(GameManager.instance._InGame1Manager._ItemDatas[i]);

            _items.Add(item);

            SimpleObjectPool<HUD_InventoryItem> itemPool = new SimpleObjectPool<HUD_InventoryItem>(item, transform, 10);
            _itemPools.Add(itemPool);
        }
    }

    public void Initialize()
    {
        gameObject.SetActive(true);
        foreach (var item in _selectedItems)
        {
            GameManager.instance._InGame1Manager._HUDInventoryPool.ReleaseItem(item._ItemData.itemID, item);
        }

        for (int i = 0; i < _enablePosition.Length; i++)
        {
            _enablePosition[i] = GameManager.instance._InGame1Manager._baseEnablePosition[i];
        }
        _selectedItems.Clear();
        _inventory.SetActive(false);
    }

    public void Reset()
    {
        gameObject.SetActive(false);
        foreach (var item in _items)
        {
            item.gameObject.SetActive(false);
        }
    }

    public HUD_InventoryItem GetItem(int index)
    {
        DevLog.Log($"GetItem: {index}");
        return _itemPools[index].Get();
    }
    public void ReleaseItem(int index, HUD_InventoryItem item)
    {
        _itemPools[index].Release(item);
    }
    
    
     void OnInventory(InputValue value)
    {
        if (_inventory.activeSelf)
        {
            _inventory.SetActive(false);
            return;
        }
        else
        {
            _inventory.SetActive(true);
            
        }
    }
}