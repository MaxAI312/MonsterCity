using System.Collections.Generic;
using UnityEngine;
using System;

public class StateMachine : MonoBehaviour
{
    [SerializeField] private UI _uI;
    [SerializeField] private MonsterSpawner _monsterSpawner;
    [SerializeField] private LevelObserver _levelObserver;

    private Dictionary<Type, IState> _statesMap;
    private IState _currentState;

    private void Awake()
    {
        InitState();
    }

    private void OnEnable()
    {
        _levelObserver.AllKilledTargets += SetEndLevelState;
    }

    private void OnDisable()
    {
        _levelObserver.AllKilledTargets -= SetEndLevelState;
    }

    private void Start()
    {
        SetStateByDefault();
    }

    private void InitState()
    {
        _statesMap = new Dictionary<Type, IState>()
        {
            [typeof(InitialState)] = new InitialState(_monsterSpawner, _levelObserver, _uI),
            [typeof(StartLevelState)] = new StartLevelState(_monsterSpawner, _levelObserver),
            [typeof(PlayState)] = new PlayState(_monsterSpawner, _levelObserver),
            [typeof(EndLevelState)] = new EndLevelState(_uI)
        };
    }

    private void SetInitialState()
    {
        var state = GetState<InitialState>();
        SetState(state);
    }

    private void SetStartLevelState()
    {
        var state = GetState<StartLevelState>();
        SetState(state);
    }

    private void SetPlayState()
    {
        var state = GetState<PlayState>();
        SetState(state);
    }

    private void SetFailState()
    {
        var state = GetState<FailState>();
        SetState(state);
    }

    private void SetEndLevelState()
    {
        Debug.Log("SetEndLevelState");
        var state = GetState<EndLevelState>();
        SetState(state);
    }

    private void SetPauseState()
    {
        var state = GetState<PauseState>();
        SetState(state);
    }

    private void SetStateByDefault()
    {
        SetPlayState();

    }

    private void SetState(IState newState)
    {
        if (_currentState != null)
            _currentState.Exit();

        _currentState = newState;
        _currentState.Enter();
    }

    private IState GetState<T>() where T : IState
    {
        var type = typeof(T);
        return _statesMap[type];
    }
}
