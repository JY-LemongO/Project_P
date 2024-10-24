using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CreatureController
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_attackCoroutine != null)
            return;

        //if (collision.CompareTag("Detector_Ally"))
        //    return;

        if (collision.TryGetComponent(out IDamagable damagable))
        {
            if (damagable.DamagableType == DamagableType.Enemy)
                return;            

            if (_target != damagable)
                _target = damagable;

            _context.StateMachine.IsInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out IDamagable damagable))
        {
            if (damagable.DamagableType == DamagableType.Enemy)
                return;

            if (_target == damagable)
                _target = null;

            _context.StateMachine.IsInRange = false;
        }
    }
}
