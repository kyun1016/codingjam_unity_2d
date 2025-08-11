using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyPoolManager : MonoBehaviour
{
    public List<EnemyPool> _enemyPools;

    public void Initialize()
    {
        foreach (var pool in _enemyPools)
        {
            pool.Initialize();
        }
    }
    public void Reset()
    {
        foreach (var pool in _enemyPools)
        {
            pool.Reset();
        }
        gameObject.SetActive(false);
    }
}