using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class HUDSkillNode : MonoBehaviour, IHUD
{
    [Header("UI Components")]
    public SkillData _skillData;
    public bool _isVisible = false;
    [Header("Skill Node Settings")]
    public Image _image;
    public TextMeshProUGUI _skillName;
    public TextMeshProUGUI _skillDescription;
    public TextMeshProUGUI _skillCount;
    public int _count;

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
        _image.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        gameObject.SetActive(true);
        DevLog.Log("HUDSkillTree: Initialized successfully");
    }
    public void Reset()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        _isVisible = true;
        gameObject.SetActive(true);
        UpdateDisplay();
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

    public void SetSkillData()
    {
        
    }
    
    public void UpdateUI() { }
    public void LateUpdateUI(){}

    #endregion

    public void Update()
    {

    }
    
    private void UpdateTimer()
    {

    }
    
    private void UpdateDisplay()
    {
        _skillData.skillName = "Skill Tree";
    }
}