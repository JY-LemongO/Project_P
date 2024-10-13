using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatTestScene : BaseScene
{
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
    }

    private void StartTestCombat()
    {
        
    }
}
