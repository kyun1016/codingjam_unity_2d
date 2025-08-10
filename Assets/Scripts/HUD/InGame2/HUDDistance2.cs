using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class HUDDistance2 : MonoBehaviour, IHUD
{
    [Header("UI Components")]
    public TextMeshProUGUI _text;
    public int _distance;

    private void Awake()
    {
    }
    
    #region IHUD Implementation
    public void ValidateComponents()
    {

    }
    public void Initialize()
    {
        gameObject.SetActive(true);
        UpdateDistanceDisplay();
    }
    public void Reset()
    {
        gameObject.SetActive(false);
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
        _distance = (int)(GameManager.instance._maxDistance - GameManager.instance._distance);
        _text.text = $"{_distance:000,000,000}";
    }
}