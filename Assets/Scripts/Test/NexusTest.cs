using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;

public struct NexusData
{
    public int dev_Id;
    public string dev_Name;

    public string description;
    public float hp;
    public float defense;
    public int capacity;

    public Sprite icon;
}

public class NexusTest : MonoBehaviour, IDamagable
{
    public Action onNexusDestroyed;

    public string Description { get; private set; }
    public float Hp { get; private set; }
    public float Defense { get; private set; }
    public int Capacity { get; private set; }

    public Transform Transform => transform;
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

    private void OnDisable()
    {
        onNexusDestroyed = null;
    }

    public void GetDamage(float damage)
    {
        if (Hp <= 0)
            return;

        Hp = Mathf.Clamp(Hp - damage, 0, int.MaxValue);
        Debug.Log(Hp);

        if(_damagingCoroutine != null)
            StopCoroutine(_damagingCoroutine);

        _damagingCoroutine = StartCoroutine(Co_FlashColor());

        if(Hp <= 0)
        {
            Debug.Log("넥서스 파괴.");
            onNexusDestroyed?.Invoke();
            gameObject.SetActive(false);
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
