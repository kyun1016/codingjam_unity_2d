using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
using Unity.VisualScripting;

public class HUDSkillTree : MonoBehaviour, IHUD
{
    [Header("UI Prefabs")]
    [SerializeField] private HUDSkillNode _skillNodePrefab;
    [SerializeField] private Image _connectorPrefab;
    [Header("UI Components")]
    [SerializeField] private TextMeshProUGUI _titleText;
    public bool _isVisible = false;

    [Header("Settings")]
    public float nodeSize = 100f; // 노드 하나의 그리드 크기
    public float zoomSpeed = 0.1f;
    public float minZoom = 0.5f;
    public float maxZoom = 2f;
    [Header("Debug")]
    [SerializeField] private PoolManager _skillNodePool;
    [SerializeField] private PoolManager _connectorPool;

    private void Awake()
    {
        ValidateComponents();
        Initialize();
    }
    
    #region IHUD Implementation
    public void ValidateComponents()
    {
        if (_titleText == null)
        {
            _titleText = GetComponentInChildren<TextMeshProUGUI>();
            if (_titleText == null)
            {
                DevLog.LogError("HUDSkillTree: TextMeshProUGUI component not found!");
            }
        }
        DevLog.Log("HUDSkillTree: Components validated");
    }
    public void Initialize()
    {
        gameObject.SetActive(true);

        _skillNodePool = new GameObject("SkillNodePool").AddComponent<PoolManager>();
        _skillNodePool.transform.SetParent(transform);
        // _connectorPool = new PoolManager(_connectorPrefab.gameObject);
        DevLog.Log("HUDSkillTree: Initialized successfully");
    }
    
    public void Show()
    {
        _isVisible = true;
        gameObject.SetActive(true);
        UpdateDisplay();
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
        if (_titleText == null) return;

        _titleText.text = "Skill Tree";
    }
}