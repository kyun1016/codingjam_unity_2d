using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // 이벤트 시스템 사용을 위해 필수!
using UnityEngine.UI;           // CanvasGroup을 사용하기 위해 필요

public class HUD_InventoryPool : MonoBehaviour
{
    [SerializeField] private ItemData[] _ItemDatas; // 마우스
    [SerializeField] private int[,] _enablePosition = new int[4, 8];
    [SerializeField] private List<int[]> _itemPosition;
    [SerializeField] private HUD_InventoryItem _itemPrefab;
    [SerializeField] private List<HUD_InventoryItem> _items;

    void Awake()
    {
        for (int i = 0; i < _ItemDatas.Length; i++)
        {
            HUD_InventoryItem item = Instantiate(_itemPrefab, transform);
            item.name = $"item_{i}";
            item.Initialize(_ItemDatas[i]);
            _items.Add(item);
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
}