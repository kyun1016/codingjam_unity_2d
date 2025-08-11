using UnityEngine;
using System;

public interface IHUD
{
    // 생명주기
    void ValidateComponents();
    void Initialize(); // 시작
    void Reset(); // 종료
    void Show();
    void Hide();
    void Toggle();
    // 업데이트
    void UpdateUI();
    void LateUpdateUI();
}