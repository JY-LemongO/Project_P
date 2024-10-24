using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyStateMachine : CreatureStateMachine
{
    public bool IsPatrol;
    public bool IsDetect;

    protected override void Initialize()
    {
        _stateStorage[State.Idle] = new AllyIdleState(this);
        _stateStorage[State.Move] = new AllyMoveState(this);
        _stateStorage[State.Attack] = new AllyAttackState(this);
    }
}
