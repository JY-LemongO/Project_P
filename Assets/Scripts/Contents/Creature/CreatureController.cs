using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class CreatureController : MonoBehaviour
{
    // [To Do]
    // 움직이는 로직은 여기서 하기
    // Ally와 Enemy 세부 로직은 상속받아서 하기
    // Creature와 상호의존 관계는 피하고.. 능력치를 Creature에서 받아와서 쓰기..?    

    public IDamagable Target => _target;
    public Coroutine AttackCoroutine
    {
        get => _attackCoroutine;
        private set
        {
            _attackCoroutine = value;
            SearchNewTarget();
        }        
    }

    protected Rigidbody2D _rigid;
    protected SpriteRenderer _rend;
    protected Creature _context;

    protected IDamagable _target;

    protected Coroutine _attackCoroutine;

    public void Init()
    {
        Debug.Log("Init 몇 번 발동?");
        _rigid = GetComponent<Rigidbody2D>();
        _rend = GetComponent<SpriteRenderer>();
        _context = GetComponent<Creature>();

        _context.StateMachine.onStateChanged += HandleAttack;
        _context.StateMachine.onStateChanged += CheckStopAttack;
    }

    // [To Do]
    // 공격, 이동 등을 인터페이스를 사용해서 어느 크리처냐에 따라 행동을 다르게 할 거니까
    // 인터페이스를 받아와서 원하는 동작을 하게끔 설정. 전략패턴

    // 하지만 지금 당장은 깡으로 모두 동일하게 설정

    private void Update()
    {
        if (!_context.IsInit)
            return;

        if (_context.StateMachine.IsDead) return;

        HandleIdle();
        HandleMove();
    }

    private void HandleIdle()
    {
        if (_context.StateMachine.CurrentState != State.Idle) return;

        if (_target == null)
        {
            if (_context.DamagableType == DamagableType.Enemy)
                _target = FindObjectOfType<NexusTest>().GetComponent<IDamagable>();
        }

        Debug.Log("Handle Idle...");
    }

    private void HandleMove()
    {
        if (_context.StateMachine.CurrentState != State.Move) return;

        if (_target == null)
        {
            Debug.LogError($"Target이 존재하지 않음.");
            return;
        }

        float moveDirX = Mathf.Sign(_target.Transform.position.x - transform.position.x);
        _rigid.velocity = new Vector2(moveDirX * _context.Speed, _rigid.velocity.y);

        Debug.Log("Handle Move...");
    }

    private void HandleAttack(State state)
    {
        if (state != State.Attack) return;

        AttackCoroutine = StartCoroutine(Co_Attack());
    }

    private IEnumerator Co_Attack()
    {
        while (true)
        {
            Debug.Log("Handle Attack...");            
            _target.GetDamage(_context.Attack);

            yield return new WaitForSeconds(_context.AtkRate);
        }        
    }

    private void CheckStopAttack(State state)
    {
        if (state != State.Attack && AttackCoroutine != null)
        {
            StopCoroutine(AttackCoroutine);
            Debug.Log("Attack Coroutine Stopped");
        }
    }

    private void SearchNewTarget()
    {

    }
}
