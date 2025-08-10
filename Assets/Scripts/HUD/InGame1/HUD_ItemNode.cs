using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class HUD_ItemNode : MonoBehaviour
{
    [Header("UI Components")]
    public ItemData _itemData;
    public Image _image;
    public TextMeshProUGUI _itemCount;
    public int _count;
    
    public void Initialize()
    {
        _image.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        gameObject.SetActive(true);
        _count = 0;
        _itemCount.text = _count.ToString();
    }
    public void Reset()
    {
        gameObject.SetActive(false);
    }
    public void SetItemData(ItemData itemData)
    {
        _image.color = Color.white;
        _itemData = itemData;
        _count = 1;
        UpdateDisplay();
    }
    public void SetCount(int count)
    {
        _count = count;
        UpdateDisplay();
    }
    private void UpdateDisplay()
    {
        _image.sprite = _itemData.image;
        _itemCount.text = _count.ToString();
    }
}