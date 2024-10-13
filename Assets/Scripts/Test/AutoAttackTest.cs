using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoAttackTest : MonoBehaviour
{
    enum State
    {
        Idle,
        Move,
        Attack,
    }

    private State state;

    [SerializeField] float hp;
    [SerializeField] float attack;
    [SerializeField] float speed;
    [SerializeField] float atkRate;

    private IDamagable target;

    private void Start()
    {
        Idle();

        target = FindObjectOfType<NexusTest>().GetComponent<IDamagable>();
        if(target == null)
        {
            Debug.LogError("넥서스가 없습니다.");
        }
        else
        {
            state = State.Move;
        }        
    }

    private void Update()
    {
        if(state == State.Move)
        {
            Move();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out IDamagable damagable))
        {
            if(target != null && target.DamagableType != DamagableType.Nexus)
            {
                target = damagable;
            }
            StopAllCoroutines();
            Attack(damagable);
        }
    }    

    private void Idle()
    {
        Debug.Log($"{GetType()}::상태 진입 - Idle");
        state = State.Idle;
    }

    private void Move()
    {
        Debug.Log($"{GetType()}::상태 진입 - Move");
        state = State.Move;

        Vector3 moveDir = (target.Transform.position - transform.position).normalized;

        transform.position += moveDir * speed * Time.deltaTime;
    }

    private void Attack(IDamagable damagable)
    {
        Debug.Log($"{GetType()}::상태 진입 - Attack");
        state = State.Attack;

        StartCoroutine(Co_Attack(damagable));
    }

    private IEnumerator Co_Attack(IDamagable damagable)
    {
        while (state == State.Attack)
        {
            damagable.GetDamage(attack);

            yield return new WaitForSeconds(atkRate);
        }
    }
}
