using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HUDBuffer1Manager : MonoBehaviour
{
    [Header("Control")]
    public bool _isActive = false;
    public float _maxTime = 50f;
    public float _currentTime = 0f;
    public TextMeshProUGUI _descriptionText;
    [TextArea]
    public string[] _descriptionTexts;

    public void Initialize()
    {
        gameObject.SetActive(true);
        _descriptionText.text = _descriptionTexts[Random.Range(0, _descriptionTexts.Length)];
        _isActive = true;
        _currentTime = 0f;
    }
    public void Update()
    {
        if (!_isActive)
            return;
        _currentTime += Time.deltaTime;
    }

    public void Reset()
    {
        _isActive = false;
        gameObject.SetActive(false);
    }
}