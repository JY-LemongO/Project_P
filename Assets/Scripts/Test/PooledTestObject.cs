using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledTestObject : MonoBehaviour, IPoolableObject
{
    public float speed;
    public ResourceType resourceType;

    public ResourceType ResourceType => resourceType;

    private void Update()
    {
        transform.position += Vector3.up * Time.deltaTime * speed;

        if (transform.position.y > 5)
            ReturnToPool();
    }

    public void ReturnToPool()
    {
        transform.position = Vector3.zero;
        PoolManager.Instance.ReturnToPool(gameObject);
    }
}
