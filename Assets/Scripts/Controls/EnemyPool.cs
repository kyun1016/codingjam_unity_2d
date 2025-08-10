using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyPool : MonoBehaviour
{
    public Enemy _PoolPrefab;
    public List<Enemy> _Pool = new List<Enemy>();
    public int _positionIndex = 0;
    public void Initialize()
    {
        gameObject.SetActive(true);
        _Pool.Add(_PoolPrefab);
        _Pool[0].gameObject.SetActive(false);
    }
    public void Reset()
    {
        gameObject.SetActive(false);
        _positionIndex = 0;
        foreach (Enemy item in _Pool)
        {
            item.gameObject.SetActive(false);
            item.Reset();
        }
    }

    public Enemy Get()
    {
        Enemy select = null;

        foreach (Enemy item in _Pool)
        {
            if (!item.gameObject.activeSelf)
            {
                select = item;
                select.Reset();
                select.gameObject.SetActive(true);
                return select;
            }
        }

        // ... 못찾은 경우, 풀 등록
        select = Instantiate(_Pool[0], transform);
        select.name = _Pool[0].name;
        select.Reset();
        select.gameObject.SetActive(true);
        _Pool.Add(select);

        return select;
    }

    void LateUpdate()
    {
        if (_positionIndex >= _PoolPrefab._enemyData.spawnLocation.Length || GameManager.instance._distance < _PoolPrefab._enemyData.spawnLocation[_positionIndex])
            return;
        _positionIndex++;
        Get();
    }
}