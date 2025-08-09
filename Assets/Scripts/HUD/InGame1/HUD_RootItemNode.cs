using UnityEngine;
using UnityEngine.EventSystems; // 이벤트 시스템 사용을 위해 필수!
using UnityEngine.UI;           // CanvasGroup을 사용하기 위해 필요
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class HUD_RootItemNode : MonoBehaviour
{
    [SerializeField] private ItemData _itemData;
    [SerializeField] private Button _button;
    [SerializeField] private bool _isActive;

    void Awake()
    {
        _button = GetComponent<Button>();
    }

    public void Initialize()
    {
        gameObject.SetActive(true);

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

    public void OnClick(BaseEventData eventData)
    {
        if (_isActive)
        {

            // 아이템 창에서 블록을 하나 선택해서
            HUD_InventoryItem item = GameManager.instance._InGame1Manager._HUDInventoryPool.GetItem(_itemData.itemID);
            item.transform.position = _button.transform.position;

            PointerEventData pointerEventData = eventData as PointerEventData;
            
            if (item._nodes[0] is IPointerDownHandler pointerdownHandler)
            {
                pointerdownHandler.OnPointerDown(pointerEventData);
            }
            if (item._nodes[0] is IBeginDragHandler beginDragHandler)
            {
                beginDragHandler.OnBeginDrag(pointerEventData);
            }
            EventSystem.current.SetSelectedGameObject(item._nodes[0].gameObject, pointerEventData);
            
            StartCoroutine(ContinuousDragTracking(item._nodes[0], pointerEventData));

            gameObject.SetActive(false);
        }

        _button.OnDeselect(null);
    }
    

    private IEnumerator ContinuousDragTracking(HUD_InventoryItemNode node, PointerEventData initialData)
    {
        Vector2 lastMousePosition = Mouse.current.position.ReadValue();
        
        // 마우스 버튼이 눌려있거나, 실제로 드래그 중인 동안 계속
        while (Mouse.current.leftButton.isPressed || initialData.dragging)
        {
            Vector2 currentMousePosition = Mouse.current.position.ReadValue();
            Vector2 delta = currentMousePosition - lastMousePosition;

            // 움직임이 있을 때만 OnDrag 호출
            if (delta.magnitude > 0.1f)
            {
                PointerEventData continuousDragData = new PointerEventData(EventSystem.current)
                {
                    position = currentMousePosition,
                    delta = delta,
                    button = PointerEventData.InputButton.Left,
                    pointerId = -1,
                    pointerDrag = node.gameObject,
                    dragging = true
                };

                // OnDrag 지속적으로 호출
                if (node is IDragHandler dragHandler)
                {
                    dragHandler.OnDrag(continuousDragData);
                }

                lastMousePosition = currentMousePosition;
            }

            // 마우스 버튼을 뗐는지 확인
            if (!Mouse.current.leftButton.isPressed)
            {
                break;
            }

            yield return null; // 다음 프레임까지 대기
        }

        EndDragSequence(node, initialData);
    }

    private void EndDragSequence(HUD_InventoryItemNode node, PointerEventData eventData)
    {
        // OnEndDrag 호출
        if (node is IEndDragHandler endDragHandler)
        {
            PointerEventData endData = new PointerEventData(EventSystem.current)
            {
                position = Mouse.current.position.ReadValue(),
                button = PointerEventData.InputButton.Left,
                pointerId = -1,
                pointerDrag = node.gameObject,
                dragging = false
            };

            endDragHandler.OnEndDrag(endData);
        }

        // EventSystem 정리
        EventSystem.current.SetSelectedGameObject(null);
        
        Debug.Log("Drag sequence completed");
    }
}