using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyController : CreatureController
{
    [SerializeField] Collider2D _detectionCollider;
    [SerializeField] Collider2D _rangeCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (AttackCoroutine != null)
            return;

        if (collision.bounds.Intersects(_detectionCollider.bounds))
        {
            // 감지 콜라이더에 걸림. 추적모드
        }

        if (collision.bounds.Intersects(_rangeCollider.bounds))
        {
            if (collision.TryGetComponent(out IDamagable damagable))
            {
                if (damagable.DamagableType != DamagableType.Enemy)
                    return;

                Debug.Log("걸리긴 걸림?");

                if (_target != damagable)
                {
                    _target = damagable;
                    Debug.Log("새로운 타겟");
                }
                    

                _context.StateMachine.IsInRange = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.bounds.Intersects(_rangeCollider.bounds))
        {
            if (collision.TryGetComponent(out IDamagable damagable))
            {
                if (damagable.DamagableType != DamagableType.Enemy)
                    return;

                if (_target == damagable)
                    _target = null;

                _context.StateMachine.IsInRange = false;
            }
        }
    }
}
