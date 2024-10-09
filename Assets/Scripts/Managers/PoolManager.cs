using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

// 실제 오브젝트가 들어있는 풀.
// 풀 역할만 하기 때문에 간단하게 GameObject를 Queue에서 꺼내거나 돌려놓거나 없으면 만들거나 정도의 기능만 한다.
public class ObjectPool
{
    private GameObject _originPrefab;
    private Queue<GameObject> _objectQueue = new Queue<GameObject>();

    private const int INIT_CREATE_COUNT = 3;

    public void Init(Transform rootTrs, string originPath)
    {
        // Prefabs 폴더 안에 다 집어넣지도 않을 뿐더러 종류에 따라 폴더가 다 다르기 때문에 개선필수.
        _originPrefab = Resources.Load<GameObject>($"Prefabs/{originPath}");

        for (int i = 0; i < INIT_CREATE_COUNT; i++)
        {
            // 가능하다면 만든 직후에 IPoolableObject 인터페이스의 ReturnPool 호출하는 쪽으로 설계

            GameObject go = Object.Instantiate(_originPrefab, rootTrs);
            go.gameObject.SetActive(false);

            _objectQueue.Enqueue(go);
        }
    }

    public GameObject Pop()
    {
        GameObject pooledObj = null;

        if (_originPrefab == null)
        {
            Debug.LogError($"{GetType()}::오리진 프리팹이 없으면 안 되는데?");
            return null;
        }

        if (_objectQueue.Count <= 0)
        {
            Create();
        }

        pooledObj = _objectQueue.Dequeue();

        return pooledObj;
    }

    public void Create()
    {
        GameObject go = Object.Instantiate(_originPrefab, PoolManager.Instance.DisableObjTrs);
        go.SetActive(false);

        _objectQueue.Enqueue(go);
    }

    public void Push(GameObject obj)
    {
        obj.SetActive(false);
        obj.transform.SetParent(PoolManager.Instance.DisableObjTrs);

        _objectQueue.Enqueue(obj);
    }
}

// [To Do]
// 나중엔 데이터 테이블을 통해 첫 풀 생성 시, 종류에 따라 InitCreateCount를 지정해줄 수 있도록 개선.

// 외부에서 PoolManager를 통해 풀링을 한다.
// PoolManager 멤버인 string,ObjectPool Dictionary에서 원하는 key에 맞는 ObjectPool 클래스에서 
public class PoolManager : Singleton<PoolManager>
{
    [field: SerializeField] public Transform ActiveObjTrs { get; private set; }
    [field: SerializeField] public Transform DisableObjTrs { get; private set; }

    // Key == 프리팹의 이름
    private Dictionary<string, ObjectPool> _poolDict = new Dictionary<string, ObjectPool>();

    public GameObject Get(string path)
    {
        GameObject obj = null;

        if (!_poolDict.ContainsKey(path))
        {
            CreatePool(path);
        }

        obj = _poolDict[path].Pop();
        obj.transform.SetParent(ActiveObjTrs);
        obj.SetActive(true);        

        return obj;
    }

    public void ReturnToPool(GameObject obj)
    {        
        int index = obj.name.IndexOf("(");
        string key = obj.name.Substring(0, index);

        _poolDict[key].Push(obj);
    }

    private void CreatePool(string path)
    {
        ObjectPool pool = new ObjectPool();
        pool.Init(DisableObjTrs, path);

        _poolDict.Add(path, pool);
    }

    public override void Clear()
    {
        // To Do - Scene 넘어갈 때 다른 씬에서 안 쓰이는 오브젝트 전부 삭제 및 Dict 초기화
    }
}
