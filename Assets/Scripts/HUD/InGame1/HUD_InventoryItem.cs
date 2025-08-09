using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // 이벤트 시스템 사용을 위해 필수!
using UnityEngine.UI;           // CanvasGroup을 사용하기 위해 필요

public class HUD_InventoryItem : MonoBehaviour
{
    [SerializeField] private HUD_InventoryItemNode _nodePrefab;
    public ItemData _ItemData; // 마우스
    public List<HUD_InventoryItemNode> _nodes;
    [SerializeField] private Transform _transform; // 마우스 오버 시 크기 증가 비율
    [SerializeField] private Vector3 _originalScale;
    public RectTransform _rectTransform;
    [SerializeField] private Canvas _canvas;

    void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
        _canvas = GetComponentInParent<Canvas>();
        _transform = transform;
        _originalScale = _transform.localScale;
    }

    public void Initialize(ItemData itemData)
    {
        _ItemData = itemData;
        for (int i = 0; i < _ItemData.gridPosition.Length; i++)
        {
            HUD_InventoryItemNode node = Instantiate(_nodePrefab, transform);
            node.name = $"Node_{i}";
            node.Initialize(_ItemData.icon[i], _ItemData.gridPosition[i]);
            _nodes.Add(node);
        }
    }

    public Vector2Int FindPosition()
    {
        Vector2Int gridPosition = new Vector2Int(Mathf.FloorToInt((_rectTransform.anchoredPosition.x + 10) / -21f),
                                         Mathf.FloorToInt((_rectTransform.anchoredPosition.y + 10) / -21f));
        DevLog.Log($"Node Position: {gridPosition}"); // Debug log for position
        return gridPosition;
    }
    public void PointerEnter()
    {
        _transform.localScale = _originalScale * 1.1f;
    }

    public void PointerExit()
    {
        _transform.localScale = _originalScale;
    }

    public void Drag(Vector2 delta)
    {
        _rectTransform.anchoredPosition += delta / _canvas.scaleFactor;
    }

    public void Drop()
    {
        _rectTransform.anchoredPosition = new Vector2(Mathf.Round((_rectTransform.anchoredPosition.x + 10) / 21f) * 21f - 11,
                                                      Mathf.Round((_rectTransform.anchoredPosition.y + 10) / 21f) * 21f - 10);
    }
}