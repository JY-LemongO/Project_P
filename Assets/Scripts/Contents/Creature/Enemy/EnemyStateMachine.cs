using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine : CreatureStateMachine
{
    protected override void Initialize()
    {
        _stateStorage[State.Idle] = new EnemyIdleState(this);
        _stateStorage[State.Move] = new EnemyMoveState(this);
        _stateStorage[State.Attack] = new EnemyAttackState(this);
    }
}
