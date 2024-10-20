using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackState : BaseState
{
    public EnemyAttackState(CreatureStateMachine context) : base(context)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        _context.IsAttack = true;
        _context.Context.Anim.SetBool("Attack", true);
    }

    public override void Update()
    {
        base.Update();

        CheckSwitchState();
    }

    public override void CheckSwitchState()
    {
        base.CheckSwitchState();

        if (!_context.IsInRange)
            SwitchState(State.Idle);
    }

    public override void ExitState()
    {
        base.ExitState();

        _context.IsAttack = false;
        _context.Context.Anim.SetBool("Attack", false);
    }
}
