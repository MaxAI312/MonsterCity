using UnityEngine;

public class Npc : Unit
{
    [SerializeField] private NpcRadar _radar;

    public NpcRadar Radar => _radar;
    public int Reward { get; private set; } = 5;

    public override void Awake()
    {
        base.Awake();
        _radar.MonsterTaken += OnMonsterTaken;
    }

    public override void Start() { }

    public override void SetFindTargetState()
    {
        Animator.PlayIdle();
        SetMoveState(Target);
    }

    public override void Die()
    {
        _radar.MonsterTaken -= OnMonsterTaken;
        base.Die();
    }

    private void OnMonsterTaken(Monster monster)
    {
        Target = monster;
        Target.Died += OnDied;
        SetFindTargetState();
    }
}