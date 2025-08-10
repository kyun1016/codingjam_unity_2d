using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyPool : MonoBehaviour
{
    public EnemyPoolManager[] _enemyPool;
    public Enemy[] _enemyPrefabs;
    private void Start()
    {
        // for (int i = 0; i < _enemyPrefabs.Length; i++)
        // {
        //     _enemyPool[i]._PoolPrefab = _enemyPrefabs[i];
        //     _enemyPool[i].transform.parent = transform;
        //     _enemyPool[i].Init();
        // }
    }
}