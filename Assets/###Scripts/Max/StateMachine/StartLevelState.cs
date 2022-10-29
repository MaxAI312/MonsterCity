using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartLevelState : IState
{
    private readonly MonsterSpawner _monsterSpawner;
    private readonly LevelObserver _levelObserver;

    public StartLevelState(MonsterSpawner monsterSpawner, LevelObserver levelObserver)
    {
        _monsterSpawner = monsterSpawner;
        _levelObserver = levelObserver;
    }

    public void Enter()
    {

    }

    public void Exit()
    {

    }
}
