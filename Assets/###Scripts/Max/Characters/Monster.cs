using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;
using DG.Tweening;

public class Monster : Unit
{
    public override void SetFindTargetState()
    {
        Animator.PlayIdle();
        Target = LevelObserver.FindNearestNpc(this);
        if(Target != null) Target.Died += OnDied;
        SetMoveState(Target);
    }

















    //[SerializeField] private ParticleSystem _particleSystem;
    //[SerializeField] private Ragdoll _ragdoll;
    //[SerializeField] private UnitAnimator _animator;
    //[SerializeField] private NavMeshAgent _navMeshAgent;
    //[SerializeField] private Collider _bodyCollider;
    //[SerializeField] private float _damage;
    //[SerializeField] private float _health;

    //private LevelObserver _levelObserver;
    //private Npc _target = null;

    //public bool IsAttack { get; private set; } = false;
    //public float Health => _health;

    //public event Action<Monster> DiedMonster;

    //private void Awake()
    //{
    //    _levelObserver = FindObjectOfType<LevelObserver>();
    //}

    //private void Start()
    //{
    //    SetFindTargetState();
    //}

    //private void OnTriggerEnter(Collider colliderBody)
    //{
    //    if (colliderBody.gameObject.TryGetComponent(out Npc target))
    //    {
    //        _target = target;
    //        IsAttack = true;
    //        target.DiedNPC += OnDiedNpc;
    //        //_attackCollider.enabled = true;
    //        SetAttackState();
    //    }

    //    if (colliderBody.gameObject.TryGetComponent(out Obstacle obstacle))
    //        obstacle.TryDestroy();
    //}

    //public void SetFindTargetState()
    //{
    //    _animator.PlayIdle();
    //    _target = _levelObserver.FindNearestNpc(this);
    //    SetMoveState(_target);
    //}

    //public void TakeDamage(float damage)
    //{
    //    _health -= damage;
    //    if (Health <= 0)
    //    {
    //        DiedMonster?.Invoke(this);
    //        _bodyCollider.enabled = false;
    //        _particleSystem.Play();
    //        _ragdoll.MakePhysics();
    //        //Destroy(gameObject, 0f);

    //    }
    //}

    //public void HitEnemy()
    //{
    //    //Debug.Log("HitEnemy");
    //    _target.TakeDamage(40);
    //}

    ////public void Init(UpgradeCardOptions upgradeCardOptions)
    ////{
    ////    _damage = upgradeCardOptions.Damage;
    ////}

    //private void SetAttackState()
    //{
    //    _navMeshAgent.enabled = false;
    //    _animator.PlayAttack();
    //}

    //private void SetMoveState(Npc target)
    //{
    //    _navMeshAgent.enabled = true;
    //    _navMeshAgent.SetDestination(target.transform.position);
    //    //_animator.Run(_navMeshAgent.velocity.magnitude);
    //    _animator.PlayRun();
    //}

    //private void OnDiedNpc(Npc npc)
    //{
    //    IsAttack = false;
    //    _levelObserver.RemoveNpc(npc);
    //    npc.DiedNPC -= OnDiedNpc;
    //}

}