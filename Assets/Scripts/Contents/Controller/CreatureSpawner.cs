using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawner : Singleton<CreatureSpawner>
{


    protected override void Init()
    {
        _isDontDestroy = true;

        base.Init();
    }
}
