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
        objects.Add(PoolManager.Instance.Get(circlePath));        
    }

    public void Spawn_Square()
    {
        objects.Add(PoolManager.Instance.Get(squarePath));        
    }

    public void Spawn_Triangle()
    {
        objects.Add(PoolManager.Instance.Get(trianglePath));        
    }

    public void DispawnAllObjects()
    {
        foreach (GameObject obj in objects)
            PoolManager.Instance.ReturnToPool(obj);        
    }
}
