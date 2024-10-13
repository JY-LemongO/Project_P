using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ResourceType
{
    Managers,
    UI,
    GameObject,
    TestObject,
}

public class ResourceManager : Singleton<ResourceManager>
{
    // 결국 풀링이던 일단 오브젝트던 Resources.Load를 클래스에서 바로 하는게 아니라 여기를 거치게 만들어야 함.

    // [To Do]
    // 1. Resources.Load 버전
    // 2. Addressable 버전
    //github desktop 연동 테스트
    private Dictionary<string, UnityEngine.Object> _loadedResourceDict = new Dictionary<string, UnityEngine.Object>();

    private const string RESOURCES_LOAD_PATH = "Prefabs";

    public T Load<T>(ResourceType resourceType, string key) where T : UnityEngine.Object
    {
        T type = null;

        if (_loadedResourceDict.ContainsKey(key))
        {            
            return type = _loadedResourceDict[key] as T;
        }

        type = Resources.Load<T>($"{RESOURCES_LOAD_PATH}/{resourceType.ToString()}/{key}");

        if (type == null)
        {
            Debug.LogError($"경로가 잘못되었거나 리소스가 없습니다. ::{type.ToString()}");
            return null;
        }

        // 어드레서블을 사용하지 않기 때문에 로드할 때 추가해준다.
        _loadedResourceDict.Add(key, type);

        return type;
    }

    public GameObject Instantiate(ResourceType resourceType, string key, Transform parent = null, bool isPooling = false)
    {
        GameObject prefab = Load<GameObject>(resourceType, key);

        if (prefab == null)
        {
            Debug.LogError($"프리팹 로드에 실패했습니다. ::{key}");
            return null;
        }

        if (isPooling)
        {
            return PoolManager.Instance.Get(prefab);
        }

        GameObject go = UnityEngine.Object.Instantiate(prefab, parent);
        go.name = prefab.name;

        return go;
    }
}
