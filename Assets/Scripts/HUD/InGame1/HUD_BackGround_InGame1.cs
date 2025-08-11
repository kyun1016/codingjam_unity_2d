using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.Assertions;

public class HUD_BackGround_InGame1 : MonoBehaviour
{
    public List<HUD_RootItemPool> _rootItemPools;
    public List<Sprite> _backGroundSprites;
    [SerializeField] private Image _image;
    [SerializeField] private Button _leftButton;
    [SerializeField] private Button _rightButton;
    public int index;

    private void Awake()
    {
        gameObject.SetActive(true);
        if (_image == null)
        {
            _image = GetComponent<Image>();
        }
        Assert.IsTrue(_rootItemPools.Count == _backGroundSprites.Count, "Root item pools and background sprites count mismatch.");
    }
    public void Initialize()
    {
        gameObject.SetActive(true);
        foreach (var item in _rootItemPools)
        {
            item.Initialize();
        }
        index = _backGroundSprites.Count / 2;
        _rootItemPools[index].gameObject.SetActive(true);
        _image.sprite = _backGroundSprites[index];
    }
    public void Reset()
    {
        gameObject.SetActive(false);
    }

    public void TriggerLeft()
    {
        if (index == 0)
            return;
        _rootItemPools[index].gameObject.SetActive(false);
        index--;
        _rootItemPools[index].gameObject.SetActive(true);
        _image.sprite = _backGroundSprites[index];
        _leftButton.OnDeselect(null);

        if (index == 0)
            _leftButton.interactable = false;
        _rightButton.interactable = true;
    }
    public void TriggerRight()
    {
        if (index == _backGroundSprites.Count - 1)
            return;
        _rootItemPools[index].gameObject.SetActive(false);
        index++;
        _rootItemPools[index].gameObject.SetActive(true);
        _image.sprite = _backGroundSprites[index];
        _rightButton.OnDeselect(null);

        if (index == _backGroundSprites.Count - 1)
            _rightButton.interactable = false;
        _leftButton.interactable = true;
    }
}