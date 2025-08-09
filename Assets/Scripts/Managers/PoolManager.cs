using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 2. 풀 담당을 하는 리스트들
    public GameObject _PoolPrefab;
    public List<GameObject> _Pool;

    // 적을 소환하는 경우 처리해야하는 데이터
    // 1. 위치 정보
    // 2. 스프라이트 정보
    // 3. 콜라이더 폴리곤 정보 <- 해당 데이터를 고려하면, 모든 타입에 대해 프리팹을 만들어서 등록해두자. 그리고 단순히 반복적으로 꺼내는 형식으로 가져가자.

    public PoolManager(GameObject prefab)
    {
        _PoolPrefab = prefab;
        Init();
    }

    public void Init()
    {
        _Pool = new List<GameObject>();
        _Pool.Add(Instantiate(_PoolPrefab, transform));
        _Pool[0].SetActive(false);
    }

    public GameObject Get()
    {
        GameObject select = null;

        // ... 선택한 풀의 놀고 (비활성화 된) 있는 게임 오브젝트 접근
        foreach (GameObject item in _Pool)
        {
            // ... 발견하면 select 변수에 할당
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                return select;
            }
        }

        // ... 못찾은 경우, 풀 등록
        select = Instantiate(_Pool[0], transform);
        select.name = _Pool[0].name;
        select.SetActive(true);
        _Pool.Add(select);

        return select;
    }
}


// public class SimpleObjectPool<T> where T : MonoBehaviour
// {
//     private T _prefab;
//     private Transform _parent;
//     private Queue<T> _pool = new Queue<T>();

//     public SimpleObjectPool(T prefab, Transform parent, int initialSize)
//     {
//         _prefab = prefab;
//         _parent = parent;
//         for (int i = 0; i < initialSize; i++)
//         {
//             T newObj = GameObject.Instantiate(_prefab, _parent);
//             newObj.gameObject.SetActive(false);
//             _pool.Enqueue(newObj);
//         }
//     }

//     public T Get()
//     {
//         if (_pool.Count > 0)
//         {
//             T obj = _pool.Dequeue();
//             obj.gameObject.SetActive(true);
//             return obj;
//         }
//         else
//         {
//             T newObj = GameObject.Instantiate(_prefab, _parent);
//             return newObj;
//         }
//     }

//     public void Release(T obj)
//     {
//         obj.gameObject.SetActive(false);
//         _pool.Enqueue(obj);
//     }
// }