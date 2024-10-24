using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllyIdleState : BaseState
{
    public AllyIdleState(CreatureStateMachine context) : base(context)
    {
    }

    public override void EnterState()
    {
        base.EnterState();

        _context.Context.Anim.SetBool("Idle", true);
    }

    public override void Update()
    {
        base.Update();

        CheckSwitchState();
    }

    public override void CheckSwitchState()
    {
        base.CheckSwitchState();

        if (_context.Context.Controller.Target != null)
            SwitchState(State.Move);
    }

    public override void ExitState()
    {
        base.ExitState();

        _context.Context.Anim.SetBool("Idle", false);
    }
}
