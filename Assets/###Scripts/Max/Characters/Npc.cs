using System;
using UnityEngine;
using UnityEngine.AI;

public class Npc : Unit
{
    [SerializeField] private NpcRadar _radar;

    public NpcRadar Radar => _radar;
    public int Raward { get; private set; } = 5;

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
        //Debug.Log("OnMonsterTaken");
        Target = monster;
        Target.Died += OnDied;
        SetFindTargetState();
    }











    //[SerializeField] private ParticleSystem _particleSystem;
    //[SerializeField] private Ragdoll _ragdoll;
    //[SerializeField] private UnitAnimator _animator;
    //[SerializeField] private NavMeshAgent _navMeshAgent;
    //[SerializeField] private Collider _bodyCollider;
    //[SerializeField] private float _damage;
    //[SerializeField] private float _health;
    //[SerializeField] private NpcRadar _radar;

    //private Vector3 _currentPositionTarget;
    //private LevelObserver _levelObserver;
    //private Monster _target = null;
    //public int Raward { get; private set; } = 5;
    //public bool IsAttack { get; private set; } = false;
    //public float Health => _health;
    //public Monster Target => _target;

    //public event Action<Npc> DiedNPC;

    //private void Awake()
    //{
    //    _levelObserver = FindObjectOfType<LevelObserver>();
    //    _radar.MonsterTaken += OnMonsterTaken;
    //}

    //private void Update()
    //{
    //    //if (_target != null)
    //    //    _currentPositionTarget = _target.transform.position;

    //    //Debug.Log(_currentPositionTarget);
    //}

    //private void OnTriggerEnter(Collider colliderBody)
    //{
    //    if (colliderBody.gameObject.TryGetComponent(out Monster target))
    //    {
    //        _target = target;
    //        IsAttack = true;
    //        target.DiedMonster += OnDiedMonster;
    //        //_attackCollider.enabled = true;
    //        SetAttackState();
    //    }

    //    //if (colliderBody.gameObject.TryGetComponent(out Obstacle obstacle))
    //    //    obstacle.TryDestroy();
    //}

    //public void SetFindTargetState()
    //{
    //    _animator.PlayIdle();

    //    //if (Vector3.Distance(_target.transform.position, transform.position) < 20f)
    //    //    SetMoveState(_target);

    //    SetMoveState(_target);
    //}

    //public void HitEnemy()
    //{
    //    _target.TakeDamage(30);
    //}

    //public void TakeDamage(float damage)
    //{
    //    _health -= damage;
    //    if (Health <= 0)
    //    {
    //        _radar.MonsterTaken -= OnMonsterTaken;
    //        DiedNPC?.Invoke(this);
    //        _bodyCollider.enabled = false;
    //        _particleSystem.Play();
    //        _ragdoll.MakePhysics();
    //    }

    //}

    ////private void CheckHealth()
    ////{
    ////    if (Health <= 0)
    ////    {
    ////        _radar.MonsterTaken -= OnMonsterTaken;
    ////        DiedNPC?.Invoke(this);
    ////        _bodyCollider.enabled = false;
    ////        _particleSystem.Play();
    ////        _ragdoll.MakePhysics();
    ////    }
    ////}

    //private void OnMonsterTaken(Monster monster)
    //{
    //    Debug.Log("OnMonsterTaken");
    //    _target = monster;
    //    SetFindTargetState();
    //}

    //private void SetAttackState()
    //{
    //    _navMeshAgent.enabled = false;
    //    //_animator.LaunchAttack();
    //    _animator.PlayAttack();
    //}

    //private void SetMoveState(Monster target)
    //{
    //    Debug.Log(this);
    //    transform.LookAt(target.transform);
    //    _navMeshAgent.enabled = true;
    //    _navMeshAgent.SetDestination(target.transform.position);
    //    //_navMeshAgent.SetDestination(target.);
    //    _animator.PlayRun();
    //}

    //private Vector3 GetCurrentPositionTarget()
    //{
    //    return _currentPositionTarget;
    //}

    //private void OnDiedMonster(Monster monster)
    //{
    //    Debug.Log("OnDiedMonster");
    //    IsAttack = false;
    //    //monster.TryDestroySelf();
    //    _levelObserver.RemoveMonster(monster);

    //    monster.DiedMonster -= OnDiedMonster;
    //}

}