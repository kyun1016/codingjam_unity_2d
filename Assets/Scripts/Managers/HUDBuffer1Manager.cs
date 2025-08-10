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
    public float _maxTime = 4f;
    public float _currentTime = 0f;
    public Image _image;

    public void Initialize()
    {
        gameObject.SetActive(true);
        _isActive = true;
        _currentTime = 0f;
        // _gemini.SendChat();
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