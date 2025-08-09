using UnityEngine;
using UnityEngine.EventSystems; // 이벤트 시스템 사용을 위해 필수!
using UnityEngine.UI;           // CanvasGroup을 사용하기 위해 필요
using UnityEngine.InputSystem;

public class HUD_InventoryItemNode : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IDropHandler
{
    public HUD_InventoryItem _parent;
    public RectTransform _rectTransform;
    [SerializeField] private Image _image;
    public Vector2Int _gridPosition; // 논리적 좌표
    void Awake()
    {
        _parent = GetComponentInParent<HUD_InventoryItem>();
        _rectTransform = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
    }

    public void Initialize(Sprite sprite, Vector2Int gridPosition)
    {
        _image.sprite = sprite;
        _gridPosition = gridPosition;
        SetPosition(_gridPosition);
    }

    public void SetPosition(Vector2Int position)
    {
        _gridPosition = position;
        _rectTransform.anchoredPosition = new Vector2(-21f, -21f) * _gridPosition;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _parent.PointerExit();
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _parent.PointerEnter();
    }
    public void OnDrag(PointerEventData eventData)
    {
        DevLog.Log($"Dragging Node at position: {eventData.delta}"); // Debug log for dragging
        _parent.Drag(eventData.delta);
    }
    public void OnDrop(PointerEventData eventData)
    {
        _parent.Drop();
    }
}