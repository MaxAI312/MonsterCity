using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayState : IState
{
    private readonly MonsterSpawner _monsterSpawner;
    private readonly LevelObserver _levelObserver;

    public PlayState(MonsterSpawner monsterSpawner, LevelObserver levelObserver)
    {
        _monsterSpawner = monsterSpawner;
        _levelObserver = levelObserver;
    }

    public void Enter()
    {
        _levelObserver.Initialize();//перенести в InitialState
        _monsterSpawner.CameraController.Clicked += OnTrySpawn;
        //_monsterSpawner.Shop.ItemSelected += OnMonsterSelected;
    }

    public void Exit()
    {
        _monsterSpawner.CameraController.Clicked -= OnTrySpawn;
        //_monsterSpawner.Shop.ItemSelected -= OnMonsterSelected;
    }

    private void OnTrySpawn(Vector3 pointSpawn)
    {
        _monsterSpawner.TrySpawn(pointSpawn);
    }

    //private void OnMonsterSelected(IMonsterData data)
    //{
    //    Debug.Log(data);
    //    Debug.Log("OnMonsterSelected");
    //    _monsterSpawner.MonsterSelected(data);
    //}
}
