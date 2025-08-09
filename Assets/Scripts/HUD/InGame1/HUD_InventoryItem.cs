using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems; // 이벤트 시스템 사용을 위해 필수!
using UnityEngine.UI;           // CanvasGroup을 사용하기 위해 필요
using System.Collections;

public class HUD_InventoryItem : MonoBehaviour
{
    [SerializeField] private HUD_InventoryItemNode _nodePrefab;
    public ItemData _ItemData; // 마우스
    public List<HUD_InventoryItemNode> _nodes;
    [SerializeField] private Transform _transform; // 마우스 오버 시 크기 증가 비율
    [SerializeField] private Vector3 _originalScale;
    public RectTransform _rectTransform;
    [SerializeField] private Canvas _canvas;
    public int _handleIndex;

    private bool _isDropped;

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
        _isDropped = false;
        for (int i = 0; i < _ItemData.gridPosition.Length; i++)
        {
            HUD_InventoryItemNode node = Instantiate(_nodePrefab, transform);
            node.name = $"Node_{i}";
            node.Initialize(_ItemData.icon[i], _ItemData.gridPosition[i]);
            _nodes.Add(node);
        }
        gameObject.SetActive(false);
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
        if (_isDropped)
            return;
        _transform.localScale = _originalScale * 1.1f;
    }

    public void PointerExit()
    {
        if (_isDropped)
            return;
        _transform.localScale = _originalScale;
    }

    public void Drag(Vector2 delta)
    {
        if (_isDropped)
            return;
        GameManager.instance._InGame1Manager._HUDInventoryPool._inventory.SetActive(true);
        foreach (var item in GameManager.instance._InGame1Manager._HUDInventoryPool._selectedItems)
        {
            item.gameObject.SetActive(true);
        }
        _rectTransform.anchoredPosition += delta / _canvas.scaleFactor;
    }

    public void Drop()
    {
        if (_isDropped)
            return;
        _isDropped = true;
        _transform.localScale = _originalScale;

        int x = Mathf.RoundToInt((_rectTransform.anchoredPosition.x + 10) / 21f) - 1;
        int y = -Mathf.RoundToInt((_rectTransform.anchoredPosition.y + 10) / 21f);

        _rectTransform.anchoredPosition = new Vector2((x + 1) * 21f - 11,
                                                      y * -21f - 10);
        
        DevLog.Log($"Dropped at: ({x}, {y})"); // Debug log for drop position
        bool isValidPosition = true;
        foreach (var pos in _ItemData.gridPosition)
        {
            if (x < 0 || x > 8 || y < 0 || y > 4
            || !GameManager.instance._InGame1Manager._HUDInventoryPool._enablePosition[(x + pos.x) + (y + pos.y) * 8])
            {
                isValidPosition = false;
                break;
            }
        }

        if (isValidPosition)
        {
            GameManager.instance._InGame1Manager._HUDInventoryPool._selectedItems.Add(this);
            foreach (var pos in _ItemData.gridPosition)
            {
                GameManager.instance._InGame1Manager._HUDInventoryPool._enablePosition[(x + pos.x) + (y + pos.y) * 8] = true;
            }
        }
        else
        {
            GameManager.instance._InGame1Manager._HUDInventoryPool.ReleaseItem(_ItemData.itemID, this);
        }

        GameManager.instance._InGame1Manager._HUDBackGround._rootItemPools[GameManager.instance._InGame1Manager._HUDBackGround.index]._nodes[_handleIndex].gameObject.SetActive(false);
        StartCoroutine(DeactivateAfterDelay(0.3f));
    }

    private IEnumerator DeactivateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManager.instance._InGame1Manager._HUDInventoryPool._inventory.SetActive(false);
        foreach (var item in GameManager.instance._InGame1Manager._HUDInventoryPool._selectedItems)
        {
            item.gameObject.SetActive(false);
        }
        gameObject.SetActive(false);
    }
}