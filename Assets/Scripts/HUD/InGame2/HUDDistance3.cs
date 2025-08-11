using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class HUDDistance3 : MonoBehaviour, IHUD
{
    [Header("UI Components")]
    public TextMeshProUGUI _textMap;
    public TextMeshProUGUI _textLevel;
    public bool _reverse = false;
    public bool _isVisible = false;
    public int _level = 1;

    private void Awake()
    {
        ValidateComponents();
        Initialize();
    }
    
    #region IHUD Implementation
    public void ValidateComponents()
    {

    }
    public void Initialize()
    {
        gameObject.SetActive(true);
        _level = 1;
    }
    public void Reset()
    {
        gameObject.SetActive(false);
        DevLog.Log("HUDDistance: Reset successfully");
    }
    
    public void Show()
    {
        gameObject.SetActive(true);
        UpdateDistanceDisplay();
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }
    
    public void Toggle()
    {
        if (gameObject.activeSelf)
            Hide();
        else
            Show();
    }
    
    public void UpdateUI()
    {
        UpdateDistanceDisplay();
    }
    
    public void LateUpdateUI(){}

    #endregion

    public void Update()
    {
        UpdateUI();
    }

    public void UpdateDistanceDisplay()
    {
        _level = (int)(((GameManager.instance._distance + 1.0f) / 1000.0f) + 1.0f);
        _textLevel.text = $"{(_level):00}";
        if (_level == 1)
            _textMap.text = "서울";
        else if (_level == 2)
            _textMap.text = "산악";
        else
            _textMap.text = "TBD";

    }
}