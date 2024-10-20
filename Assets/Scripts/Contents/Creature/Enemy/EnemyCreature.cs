using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreature : Creature
{
    protected override void Init()
    {
        base.Init();
        
        _damagableType = DamagableType.Enemy;
    }

    public override void Setup(CreatureData data)
    {
        base.Setup(data);
    }
}
