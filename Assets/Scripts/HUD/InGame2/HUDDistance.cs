using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class HUDDistance : MonoBehaviour, IHUD
{
    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI _text;
    public bool _isVisible = false;

    private void Awake()
    {
        ValidateComponents();
        Initialize();
    }
    
    #region IHUD Implementation
    public void ValidateComponents()
    {
        if (_text == null)
        {
            _text = GetComponentInChildren<TextMeshProUGUI>();
            if (_text == null)
            {
                DevLog.LogError("HUDTimer: TextMeshProUGUI component not found!");
            }
        }
        DevLog.Log("HUDTimer: Components validated");
    }
    public void Initialize()
    {
        gameObject.SetActive(true);
    }
    public void Reset()
    {
        gameObject.SetActive(false);
        DevLog.Log("HUDDistance: Reset successfully");
    }
    
    public void Show()
    {
        _isVisible = true;
        gameObject.SetActive(true);
        UpdateDistanceDisplay();
    }
    
    public void Hide()
    {
        _isVisible = false;
        gameObject.SetActive(false);
    }
    
    public void Toggle()
    {
        if (_isVisible)
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
        if (!_isVisible)
            return;
        UpdateUI();
    }
    
    private void UpdateDistanceDisplay()
    {
        if (_text == null) return;

        _text.text = $"{Mathf.FloorToInt(GameManager.instance._distance):0000}m";
    }
}