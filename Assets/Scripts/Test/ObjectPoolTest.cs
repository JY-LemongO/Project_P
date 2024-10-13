using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolTest : MonoBehaviour
{
    public List<GameObject> objects = new List<GameObject>();
    [Space(10)]
    [SerializeField] string circlePath;
    [SerializeField] string squarePath;
    [SerializeField] string trianglePath;

    public void Spawn_Circle()
    {
        objects.Add(ResourceManager.Instance.Instantiate(ResourceType.TestObject, circlePath, isPooling: true));
    }

    public void Spawn_Square()
    {
        objects.Add(ResourceManager.Instance.Instantiate(ResourceType.TestObject, squarePath, isPooling: true));
    }

    public void Spawn_Triangle()
    {
        objects.Add(ResourceManager.Instance.Instantiate(ResourceType.TestObject, trianglePath, isPooling: true));
    }

    public void DispawnAllObjects()
    {
        foreach (GameObject obj in objects)
            PoolManager.Instance.ReturnToPool(obj);
    }
}
