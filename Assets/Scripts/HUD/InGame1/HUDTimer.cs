using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class HUDTimer : MonoBehaviour, IHUD
{
    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI _timerText;
    public float _maxTime = 600f;
    public float _currentTime = 0f;
    public bool _isVisible = false;

    private void Awake()
    {
        ValidateComponents();
        Initialize();
    }
    
    #region IHUD Implementation
    public void ValidateComponents()
    {
        if (_timerText == null)
        {
            _timerText = GetComponentInChildren<TextMeshProUGUI>();
            if (_timerText == null)
            {
                DevLog.LogError("HUDTimer: TextMeshProUGUI component not found!");
            }
        }
        DevLog.Log("HUDTimer: Components validated");
    }
    public void Initialize()
    {
        _currentTime = 0f;
        gameObject.SetActive(false);
        DevLog.Log("HUDTimer: Initialized successfully");
    }
    
    public void Show()
    {
        _isVisible = true;
        gameObject.SetActive(true);
        UpdateTimerDisplay();
        DevLog.Log("HUDTimer: Shown");
    }
    
    public void Hide()
    {
        _isVisible = false;
        gameObject.SetActive(false);
        DevLog.Log("HUDTimer: Hidden");
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
        UpdateTimer();
    }
    
    public void LateUpdateUI(){}

    #endregion

    public void Update()
    {
        if (!_isVisible)
            return;
        _currentTime += Time.deltaTime;
        UpdateTimerDisplay();
    }
    
    private void UpdateTimer()
    {
        _currentTime += Time.deltaTime;
        UpdateTimerDisplay();
    }
    
    private void UpdateTimerDisplay()
    {
        if (_timerText == null) return;
        
        float displayTime = _currentTime;
        int minutes = Mathf.FloorToInt(displayTime / 60f);
        int seconds = Mathf.FloorToInt(displayTime % 60f);
        int milliseconds = Mathf.FloorToInt((displayTime * 100f) % 100f);
        _timerText.text = $"{minutes:00}:{seconds:00}:{milliseconds:00}";
    }
}