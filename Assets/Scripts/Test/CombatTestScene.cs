using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTestScene : BaseScene
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Vector3 enemySpawnPoint;

    [SerializeField] GameObject allyPrefab;    
    [SerializeField] Vector3 allySpawnPoint;

    [field:SerializeField]
    public List<CreatureData> tempAllyCreatureDatas;

    [field: SerializeField]
    public List<CreatureData> tempEnemyCreatureDatas;

    [Header("Temp DataId")]
    public int selectedAllyID;
    public int selectedEnemyID;

    protected override void Init()
    {
        base.Init();

        NexusData nexusData = new NexusData()
        {
            dev_Id = 1,
            dev_Name = "test",

            description = "Test Nexus",
            hp = 100f,
            defense = 1f,
            capacity = 100,
            icon = null
        };

        NexusTest nexus = FindObjectOfType<NexusTest>();
        nexus.Setup(nexusData);
        nexus.onNexusDestroyed += () =>
        {
            UIBaseData data = new UIBaseData();
            UIManager.Instance.OpenUI<UI_GameOver>(data);
        };

        selectedAllyID = 10001;
        selectedEnemyID = 20001;        
    }

    private void Start()
    {
        CreatureSpawner.Instance.SetupDatas(tempAllyCreatureDatas, tempEnemyCreatureDatas);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            selectedAllyID = Mathf.Clamp(selectedAllyID + 1, 10001, 10003);
            Debug.Log($"AllyID +1 :: Now[{selectedAllyID}]");
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            selectedAllyID = Mathf.Clamp(selectedAllyID - 1, 10001, 10003);
            Debug.Log($"AllyID +1 :: Now[{selectedAllyID}]");
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            selectedEnemyID = Mathf.Clamp(selectedEnemyID + 1, 20001, 20003);
            Debug.Log($"EnemyID +1 :: Now[{selectedEnemyID}]");
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            selectedEnemyID = Mathf.Clamp(selectedEnemyID - 1, 20001, 20003);
            Debug.Log($"EnemyID +1 :: Now[{selectedEnemyID}]");
        }
    }

    public void SpawnTestAlly()
    {
        Creature clone = CreatureSpawner.Instance.SpawnCreature(selectedAllyID);
        clone.transform.position = allySpawnPoint;        
    }

    public void SpawnTestEnemy()
    {
        Creature clone = CreatureSpawner.Instance.SpawnCreature(selectedEnemyID, true);
        clone.transform.position = enemySpawnPoint;
    }
}
