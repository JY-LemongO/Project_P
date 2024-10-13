using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DamagableType
{
    Nexus,
    WildAnimal,
    Player,
    Ally,
}

public interface IDamagable
{
    DamagableType DamagableType { get; }
    void GetDamage(float damage);
}
