using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialState : IState
{
    private readonly MonsterSpawner _monsterSpawner;
    private readonly LevelObserver _levelObserver;
    private readonly UI _uI;

    public InitialState(MonsterSpawner monsterSpawner, LevelObserver levelObserver, UI uI)
    {
        _monsterSpawner = monsterSpawner;
        _levelObserver = levelObserver;
        _uI = uI;
    }

    public void Enter()
    {
        //_uI.PlayMenu.Show();
    }

    public void Exit()
    {
        
    }
}
