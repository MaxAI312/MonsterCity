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
        _levelObserver.Initialize();
        _monsterSpawner.CameraController.Clicked += OnTrySpawn;
    }

    public void Exit()
    {
        _monsterSpawner.CameraController.Clicked -= OnTrySpawn;
    }

    private void OnTrySpawn(Vector3 pointSpawn)
    {
        _monsterSpawner.TrySpawn(pointSpawn);
    }
}
