using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HUDBuffer2Manager : MonoBehaviour
{
    [Header("Control")]
    public bool _isActive = false;
    public float _maxTime = 50f;
    public float _currentTime = 0f;
    public Image _image;
    public TextMeshProUGUI _descriptionText;
    public TextMeshProUGUI _aiText;
    [TextArea]
    public string[] _descriptionTexts;
    public UnityAndGeminiV3 _gemini;
    public TextMeshProUGUI _geminiOutput;

    public void Initialize()
    {
        gameObject.SetActive(true);
        // _descriptionText.text = _descriptionTexts[Random.Range(0, _descriptionTexts.Length)];
        _aiText.text = _geminiOutput.text;
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
        // _gemini.SendChat();
    }
}