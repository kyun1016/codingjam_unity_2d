using UnityEngine;
using UnityEngine.EventSystems; // Event System을 사용하기 위해 필요

public class UIMouseHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private Transform _transform; // 마우스 오버 시 크기 증가 비율
    private Vector3 _originalScale;

    void Start()
    {
        if (_transform == null)
            _transform = transform;
        _originalScale = _transform.localScale;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _transform.localScale = _originalScale;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        _transform.localScale = _originalScale * 1.1f;
    }
}