using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyCreature : Creature
{
    protected override void Init()
    {
        base.Init();

        _damagableType = DamagableType.Ally;
    }

    public override void Setup(CreatureData data)
    {
        base.Setup(data);
    }
}
