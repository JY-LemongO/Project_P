using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public enum CreatureType
{
    Ally,
    Enemy,
}

[Serializable]
public class CreatureData
{
    public int dev_Id;
    public string dev_Name;

    public float hp;
    public float attack;
    public float defense;
    public float speed;
    public float atkRate;

    public RuntimeAnimatorController animController;
    public Sprite sprite;
}

public class Creature : MonoBehaviour, IDamagable, IPoolableObject
{
    #region Events
    public Action<float> onHealthChanged;
    #endregion

    public CreatureController Controller { get; private set; }
    public CreatureStateMachine StateMachine { get; private set; }

    public Transform Transform => transform;
    public Animator Anim => _anim;
    public DamagableType DamagableType => _damagableType;
    public ResourceType ResourceType => ResourceType.TestObject;

    public bool IsDead => _hp <= 0;

    protected DamagableType _damagableType;

    #region Stats    
    public float Hp => _hp;
    public float Attack => _attack;
    public float Defense => _defense;
    public float Speed => _speed;
    public float AtkRate => _atkRate;

    [Header("Stats")]
    // 테스트용 에디터에서 직접 입력을 위한 SerializeField
    [SerializeField] protected float _hp;
    [SerializeField] protected float _maxHp;
    [SerializeField] protected float _attack;
    [SerializeField] protected float _defense;
    [SerializeField] protected float _speed;
    [SerializeField] protected float _atkRate;
    #endregion

    private SpriteRenderer _rend;
    private Animator _anim;
    private bool _isInit = false;

    private void Awake()
    {
        Init();
    }

    protected virtual void Init()
    {
        _isInit = true;

        Controller = GetComponent<CreatureController>();
        StateMachine = GetComponent<CreatureStateMachine>();
        _anim = GetComponentInChildren<Animator>();
        _rend = GetComponentInChildren<SpriteRenderer>();

        Controller.Init();
        StateMachine.Init();
    }

    public virtual void Setup(CreatureData data)
    {
        _hp = data.hp;
        _attack = data.attack;
        _defense = data.defense;
        _speed = data.speed;
        _atkRate = data.atkRate;

        _anim.runtimeAnimatorController = data.animController;
        _rend.sprite = data.sprite;
    }

    public void GetDamage(float damage)
    {
        if (!IsDead)
            return;

        // [To Do]
        // 1. Hit sound
        _hp = Mathf.Clamp(_hp - damage, 0, int.MaxValue);
        onHealthChanged?.Invoke(_hp);

        if (_hp <= 0)
            Dead();
    }

    protected virtual void Dead()
    {
        onHealthChanged = null;

        _hp = 0;
        _maxHp = 0;
        _attack = 0;
        _defense = 0;
        _speed = 0;
        _atkRate = 0;

        _rend.sprite = null;

        ReturnToPool();
    }

    public void ReturnToPool() => PoolManager.Instance.ReturnToPool(gameObject);
}
