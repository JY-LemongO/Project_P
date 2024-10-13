using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public struct NexusData
{
    public int dev_Id;
    public string dev_Name;

    public string description;
    public int hp;
    public int defense;
    public int capacity;

    public Sprite icon;
}

public class NexusTest : MonoBehaviour, IDamagable
{
    public string Description { get; private set; }
    public float Hp { get; private set; }
    public float Defense { get; private set; }
    public int Capacity { get; private set; }

    public DamagableType DamagableType => damagableType;
    private DamagableType damagableType = DamagableType.Nexus;

    private Coroutine _damagingCoroutine;

    public void Setup(NexusData data)
    {
        Description = data.description;
        Hp = data.hp;
        Defense = data.defense;
        Capacity = data.capacity;
    }

    public void GetDamage(float damage)
    {
        if (Hp <= 0)
            return;

        Hp = Mathf.Clamp(Hp - damage, 0, int.MaxValue);

        if(_damagingCoroutine != null)
            StopCoroutine(_damagingCoroutine);

        _damagingCoroutine = StartCoroutine(Co_FlashColor());

        if(Hp <= 0)
        {
            Debug.Log("넥서스 파괴.");
        }
    }

    private IEnumerator Co_FlashColor()
    {
        SpriteRenderer rend = GetComponent<SpriteRenderer>();


        rend.color = Color.red;
        yield return new WaitForSeconds(0.5f);
        rend.color = Color.white;

    }
}
