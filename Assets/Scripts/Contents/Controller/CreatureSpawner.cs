using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureSpawner : Singleton<CreatureSpawner>
{
    [SerializeField] private Creature _allyCreature;
    [SerializeField] private Creature _enemyCreature;

    private Dictionary<int, CreatureData> _creatureDataContainer = new Dictionary<int, CreatureData>();

    protected override void Init()
    {
        _isDontDestroy = false;

        base.Init();
    }

    public void SetupDatas(List<CreatureData> allyDatas, List<CreatureData> enemyDatas)
    {
        _creatureDataContainer.Clear();
        foreach (CreatureData data in allyDatas)
            _creatureDataContainer[data.dev_Id] = data;

        foreach (CreatureData data in enemyDatas)
            _creatureDataContainer[data.dev_Id] = data;

        Debug.Log("데이터 셋업 완료.");
    }

    public Creature SpawnCreature(int dataId, bool isEnemy = false)
    {
        if (!_creatureDataContainer.ContainsKey(dataId))
        {
            Debug.LogError("ID가 없으면 안 되제");
            return null;
        }        

        CreatureData creatureData = _creatureDataContainer[dataId];
        Creature creature = PoolManager.Instance.Get(isEnemy ? _enemyCreature.gameObject : _allyCreature.gameObject).GetComponent<Creature>();
        creature.Setup(creatureData);

        return creature;
    }
}
