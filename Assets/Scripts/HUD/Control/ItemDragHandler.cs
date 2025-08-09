using UnityEngine;
using UnityEngine.EventSystems; // 이벤트 시스템 사용을 위해 필수!
using UnityEngine.UI;           // CanvasGroup을 사용하기 위해 필요

public class ItemDragHandler : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>(); 
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0.6f; 
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1.0f;
    }
}