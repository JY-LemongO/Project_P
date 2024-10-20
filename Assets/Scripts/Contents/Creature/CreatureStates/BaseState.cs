using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseState : IState
{
    public BaseState(CreatureStateMachine context) { _context = context; }

    protected CreatureStateMachine _context;

    public virtual void EnterState() { }
    public virtual void Update() { }
    public virtual void ExitState() { }
    public virtual void CheckSwitchState() { }

    public void UpdateState()
    {
        Update();
    }

    protected void SwitchState(State state) => _context.SetState(state);


}
