using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class HUDSkillNode : MonoBehaviour, IHUD
{
    [Header("UI Components")]
    [SerializeField] private SkillData _skillData;
    public bool _isVisible = false;
    [Header("Skill Node Settings")]
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _skillName;
    [SerializeField] private TextMeshProUGUI _skillDescription;

    private void Awake()
    {
        ValidateComponents();
        Initialize();
    }
    
    #region IHUD Implementation
    public void ValidateComponents()
    {
        if (_skillData == null)
        {
            _skillData = GetComponentInChildren<SkillData>();
            if (_skillData == null)
            {
                DevLog.LogError("HUDSkillTree: SkillData component not found!");
            }
        }
        if (_skillName == null)
        {
            DevLog.LogError("HUDSkillNode: TextMeshProUGUI component for skill name not found!");
        }
        if (_skillDescription == null)
        {
            DevLog.LogError("HUDSkillNode: TextMeshProUGUI component for skill description not found!");
        }
        DevLog.Log("HUDSkillTree: Components validated");
    }
    public void Initialize()
    {
        _image.sprite = _skillData.icon;
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
    
    public void UpdateUI(){}
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