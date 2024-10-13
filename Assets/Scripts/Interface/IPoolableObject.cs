using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPoolableObject
{
    ResourceType ResourceType { get; }
    void ReturnToPool();
}
