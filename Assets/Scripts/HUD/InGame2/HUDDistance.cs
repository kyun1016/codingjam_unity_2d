using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class HUDDistance : MonoBehaviour, IHUD
{
    [Header("UI Components")]
    public TextMeshProUGUI _text;
    public bool _isVisible = false;
    public int _distance;

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
        _distance = 0;
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
    
    public void UpdateDistanceDisplay()
    {
        _distance = (int)GameManager.instance._distance; // Assuming GameManager.instance._distance is the distance to display
        _text.text = $"{_distance:000,000,000}";
        // _text.text = $"{Mathf.FloorToInt(GameManager.instance._distance):0000}m";
    }
}