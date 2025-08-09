using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    // 2. Ǯ ����� �ϴ� ����Ʈ��
    public GameObject _PoolPrefab;
    public List<GameObject> _Pool;

    // ���� ��ȯ�ϴ� ��� ó���ؾ��ϴ� ������
    // 1. ��ġ ����
    // 2. ��������Ʈ ����
    // 3. �ݶ��̴� ������ ���� <- �ش� �����͸� ����ϸ�, ��� Ÿ�Կ� ���� �������� ���� ����ص���. �׸��� �ܼ��� �ݺ������� ������ �������� ��������.

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

        // ... ������ Ǯ�� ��� (��Ȱ��ȭ ��) �ִ� ���� ������Ʈ ����
        foreach (GameObject item in _Pool)
        {
            // ... �߰��ϸ� select ������ �Ҵ�
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                return select;
            }
        }

        // ... ��ã�� ���, Ǯ ���
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