using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamagableType
{
    Nexus,
    Enemy,
    Player,
    Ally,
}

public interface IDamagable
{
    DamagableType DamagableType { get; }
    Transform Transform { get; }
    void GetDamage(float damage);
}
