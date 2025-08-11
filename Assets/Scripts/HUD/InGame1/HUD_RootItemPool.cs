using UnityEngine;
using UnityEngine.EventSystems; // 이벤트 시스템 사용을 위해 필수!
using UnityEngine.UI;           // CanvasGroup을 사용하기 위해 필요
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class HUD_RootItemPool : MonoBehaviour
{
    public List<GameObject> _nodes;

    void Awake()
    {
        _nodes = new List<GameObject>();
        for(int i=0;  i< gameObject.transform.childCount; i++)
        {
            _nodes.Add(gameObject.transform.GetChild(i).gameObject);
        }
    }
    public void Initialize()
    {
        gameObject.SetActive(true);
        foreach (var node in _nodes)
        {
            HUD_RootItemNode itemNode = node.GetComponent<HUD_RootItemNode>();
            if (itemNode != null)
            {
                itemNode.Initialize();
            }
        }
        gameObject.SetActive(false);
    }
    public void Reset()
    {
        gameObject.SetActive(false);
    }
}