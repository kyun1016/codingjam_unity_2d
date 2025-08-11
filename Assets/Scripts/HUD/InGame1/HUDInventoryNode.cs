using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class HUDInventoryNode : MonoBehaviour
{
    [Header("UI Components")]
    public bool _isVisible = false;
    [Header("Skill Node Settings")]
    [SerializeField] private Image _image;
    public int index;

    private void Awake()
    {
        _image = GetComponent<Image>();
        index = transform.GetSiblingIndex();
    }
    public void Initialize()
    {
        gameObject.SetActive(true);
        if(GameManager.instance._InGame1Manager._baseEnablePosition[index])
        {
            _image.color = Color.white;
        }
        else
        {
            _image.color = new Color(0.5f, 0.5f, 0.2f, 0.5f); // 반투명 처리
        }
    }
    public void Reset()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        _isVisible = true;
        gameObject.SetActive(true);
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
}