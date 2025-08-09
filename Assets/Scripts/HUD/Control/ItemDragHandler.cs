using UnityEngine;
using UnityEngine.EventSystems; // 이벤트 시스템 사용을 위해 필수!
using UnityEngine.UI;           // CanvasGroup을 사용하기 위해 필요

public class ItemDragHandler : MonoBehaviour, IDragHandler
{
    [SerializeField] private RectTransform rectTransform;
    private Canvas canvas;

    void Awake()
    {
        if(rectTransform == null)
            rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>(); 
    }
    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }
}