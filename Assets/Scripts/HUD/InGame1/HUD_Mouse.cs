using UnityEngine;
using UnityEngine.EventSystems; // Event System을 사용하기 위해 필요

public class UIMouseHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private Vector3 originalScale;

    void Start()
    {
        // 시작할 때 원래 크기를 저장해 둡니다.
        originalScale = transform.localScale;
    }

    // 마우스가 UI 요소 위로 올라왔을 때 호출될 함수
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 크기를 1.1배로 키웁니다.
        transform.localScale = originalScale * 1.1f;
        Debug.Log("마우스가 들어왔습니다!");
    }

    // 마우스가 UI 요소 위에서 벗어났을 때 호출될 함수
    public void OnPointerExit(PointerEventData eventData)
    {
        // 크기를 원래대로 되돌립니다.
        transform.localScale = originalScale;
        Debug.Log("마우스가 나갔습니다!");
    }
}