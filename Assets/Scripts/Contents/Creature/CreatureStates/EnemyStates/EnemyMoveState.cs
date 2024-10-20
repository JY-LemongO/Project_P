using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMoveState : BaseState
{
    public EnemyMoveState(CreatureStateMachine context) : base(context)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        _context.IsMoveToTarget = true;
        _context.Context.Anim.SetBool("Move", true);
    }

    public override void Update()
    {
        base.Update();

        CheckSwitchState();
    }

    public override void CheckSwitchState()
    {
        base.CheckSwitchState();

        if (_context.IsInRange)
            SwitchState(State.Attack);
    }

    public override void ExitState()
    {
        base.ExitState();

        _context.IsMoveToTarget = false;
        _context.Context.Anim.SetBool("Move", false);
    }
}
