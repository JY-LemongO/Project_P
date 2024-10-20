using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State
{
    Idle,
    Move,
    Attack,
}

public abstract class CreatureStateMachine : MonoBehaviour
{
    #region Events
    public event Action<State> onStateChanged;
    #endregion

    public Creature Context { get; private set; }
    public State CurrentState
    {
        get => _currentStateEnum;
        private set
        {
            State prevState = _currentStateEnum;
            _currentStateEnum = value;
            onStateChanged?.Invoke(prevState);
        }
    }

    public bool IsInRange;
    public bool IsMoveToTarget;
    public bool IsAttack;
    public bool IsDead => Context.IsDead;    

    protected Dictionary<State, IState> _stateStorage = new Dictionary<State, IState>();
    protected IState _currentState;
    protected State _currentStateEnum;    

    public void Init()
    {
        Context = GetComponent<Creature>();

        Initialize();
        SetState(State.Idle);
    }
    
    protected abstract void Initialize();

    private void Update()
    {
        _currentState.UpdateState();        
    }

    private void OnDisable()
    {
        onStateChanged = null;
    }

    public void UpdateState()
    {
        if (_currentState != null)
            _currentState.Update();
    }

    public void SetState(State state)
    {
        if (!_stateStorage.ContainsKey(state))
        {
            Debug.LogError($"해당하는 키의 State가 없습니다. ::{state}");
            return;
        }

        CurrentState = state;
        IState newState = _stateStorage[state];        

        if (_currentState == null)
        {
            _currentState = newState;
            _currentState.EnterState();
            return;
        }

        SwitchState(newState);
    }

    private void SwitchState(IState state)
    {
        _currentState.ExitState();

        _currentState = state;
        _currentState.EnterState();
    }
}
