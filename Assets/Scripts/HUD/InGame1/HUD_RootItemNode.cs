using UnityEngine;
using UnityEngine.EventSystems; // 이벤트 시스템 사용을 위해 필수!
using UnityEngine.UI;           // CanvasGroup을 사용하기 위해 필요
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class HUD_RootItemNode : MonoBehaviour, IPointerDownHandler, IDragHandler, IDropHandler
{
    [SerializeField] private ItemData _itemData;
    [SerializeField] private Button _button;
    [SerializeField] private bool _isActive;
    private HUD_InventoryItem currentDraggingItem;
    public bool _isDropped;
    public int _index;

    void Awake()
    {
        _index = transform.GetSiblingIndex();
        _button = GetComponent<Button>();
    }

    public void Initialize()
    {
        gameObject.SetActive(true);

        _isDropped = false;
        currentDraggingItem = null;
        float para1 = Random.value;
        float para2 = Random.value;

        int itemType = (int)(para1 * GameManager.instance._InGame1Manager._ItemDatas.Count);
        if (itemType >= GameManager.instance._InGame1Manager._ItemDatas.Count)
        {
            itemType = GameManager.instance._InGame1Manager._ItemDatas.Count - 1;
        }

        _itemData = GameManager.instance._InGame1Manager._ItemDatas[itemType];
        if (para2 > _itemData.dropRate)
        {
            _isActive = true;
            SpriteState spriteState = _button.spriteState;
            spriteState.highlightedSprite = _itemData.image;
            _button.spriteState = spriteState;
        }
        else
        {
            _isActive = false;
            SpriteState spriteState = _button.spriteState;
            spriteState.highlightedSprite = null;
            _button.spriteState = spriteState;
        }
    }
    public void Reset()
    {
        gameObject.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_isDropped)
        {
            gameObject.SetActive(false);
            return;
        }
        if (_isActive)
        {
            _isDropped = true;
            HUD_InventoryItem item = GameManager.instance._InGame1Manager._HUDInventoryPool.GetItem(_itemData.itemID);
            currentDraggingItem = item;
            item.transform.position = _button.transform.position;

            eventData.pointerDrag = item.gameObject;
            ExecuteEvents.Execute(item.gameObject, eventData, ExecuteEvents.beginDragHandler);
        }
        _button.OnDeselect(null);
        if (currentDraggingItem == null)
            gameObject.SetActive(false);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (currentDraggingItem != null)
        {
            ExecuteEvents.Execute(currentDraggingItem._nodes[0].gameObject, eventData, ExecuteEvents.dragHandler);
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        // 끌고 있던 아이템이 있다면
        if (currentDraggingItem != null)
        {
            ExecuteEvents.Execute(currentDraggingItem._nodes[0].gameObject, eventData, ExecuteEvents.dropHandler);
            currentDraggingItem = null;
        }
        gameObject.SetActive(false);
    }
}