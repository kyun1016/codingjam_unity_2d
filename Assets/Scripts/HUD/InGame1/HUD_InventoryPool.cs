using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // 이벤트 시스템 사용을 위해 필수!
using UnityEngine.UI;           // CanvasGroup을 사용하기 위해 필요

public class HUD_InventoryPool : MonoBehaviour
{
    [SerializeField] private int[,] _enablePosition = new int[4, 8];
    [SerializeField] private List<int[]> _itemPosition;
    [SerializeField] private HUD_InventoryItem _itemPrefab;
    [SerializeField] private List<HUD_InventoryItem> _items;
    [SerializeField] private List<SimpleObjectPool<HUD_InventoryItem>> _itemPools;

    void Awake()
    {   
        _itemPools = new List<SimpleObjectPool<HUD_InventoryItem>>();
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
        foreach (var item in _items)
        {
            item.gameObject.SetActive(false);
        }
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
}