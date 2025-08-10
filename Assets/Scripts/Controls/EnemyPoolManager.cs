using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyPoolManager : MonoBehaviour
{
    // 2. 풀 담당을 하는 리스트들
    public Enemy _PoolPrefab;
    public List<Enemy> _Pool = new List<Enemy>();
    public int _positionIndex = 0;

    // 적을 소환하는 경우 처리해야하는 데이터
    // 1. 위치 정보
    // 2. 스프라이트 정보
    // 3. 콜라이더 폴리곤 정보 <- 해당 데이터를 고려하면, 모든 타입에 대해 프리팹을 만들어서 등록해두자. 그리고 단순히 반복적으로 꺼내는 형식으로 가져가자.

    public void Init()
    {
        _Pool.Add(_PoolPrefab);
        _Pool[0].gameObject.SetActive(false);
    }

    public Enemy Get()
    {
        Enemy select = null;

        // ... 선택한 풀의 놀고 (비활성화 된) 있는 게임 오브젝트 접근
        foreach (Enemy item in _Pool)
        {
            // ... 발견하면 select 변수에 할당
            if (!item.gameObject.activeSelf)
            {
                select = item;
                select.gameObject.SetActive(true);
                return select;
            }
        }

        // ... 못찾은 경우, 풀 등록
        select = Instantiate(_Pool[0], transform);
        select.name = _Pool[0].name;
        select.gameObject.SetActive(true);
        _Pool.Add(select);

        return select;
    }

    void LateUpdate()
    {
        if (!GameManager.instance._IsLive)
            return;
        if (_positionIndex >= _PoolPrefab._enemyData.spawnPositions.Length || GameManager.instance._distance < _PoolPrefab._enemyData.spawnPositions[_positionIndex])
            return;
        _positionIndex++;
        Enemy enemy = Get();
        enemy.gameObject.SetActive(false);
        enemy.Reset();
    }
}