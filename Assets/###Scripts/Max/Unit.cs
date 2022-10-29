using System;
using UnityEngine;
using UnityEngine.AI;
using DG.Tweening;

public abstract class Unit : Target
{
    [SerializeField] protected NavMeshAgent NavMeshAgent;
    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private Ragdoll _ragdoll;
    [SerializeField] private UnitAnimator _animator;
    [SerializeField] private Collider _bodyCollider;
    [SerializeField] private float _damage;
    [SerializeField] private float _health;
    [SerializeField] private float _attackDistance;

    private bool _isAlive = true;
    //[SerializeField] private CollisionHandler _collisionHandler;

    public Target Target { get; set; }
    public LevelObserver LevelObserver { get; private set; }

    public NavMeshAgent NMeshAgent => NavMeshAgent;
    
    public UnitAnimator Animator => _animator;
    //public CollisionHandler CollisionHandler => _collisionHandler;

    public bool IsAttack { get; set; }
    public float Health => _health;

    public virtual void Awake()
    {
        LevelObserver = FindObjectOfType<LevelObserver>();
    }

    public virtual void Start()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.AppendInterval(1.1f);

        sequence.AppendCallback(() =>
        {
            NMeshAgent.enabled = true;
            SetFindTargetState();
        });

        //sequence.AppendCallBack(SetFindTargetState());
        //SetFindTargetState();
    }

    private void Update()
    {
        if (Target != null && NavMeshAgent.enabled) NavMeshAgent.SetDestination(Target.transform.position);
        if (Target != null &&  Vector3.Distance(transform.position, Target.transform.position) < _attackDistance)
            SetAttackState();
    }

    public void OnTriggerEnter(Collider other)
    {
        var npc = other.GetComponent<Npc>();
        var monster = other.GetComponent<Monster>();
        var building = other.GetComponent<Building>();
        var fence = other.GetComponent<Fence>();

        if (npc)
        {
            ChangeTarget(npc);
            SetAttackState();
        }

        if (monster)
        {
            ChangeTarget(monster);
            SetAttackState();
        }

        if (this is Monster)
            if (building)
            {
                ChangeTarget(building);
                SetAttackState();
            }
            else if(fence)
            {
                ChangeTarget(fence);
                SetAttackState();
            }
    }

    public override event Action<Target> Died;

    public virtual void SetAttackState()
    {
        IsAttack = true;
        NavMeshAgent.enabled = false;
        _animator.PlayAttack();
    }

    public abstract void SetFindTargetState();

    public override void TakeDamage(float damage)
    {
        if (_isAlive)
        {
            _health -= damage;
            TryDie();
        }
    }

    public virtual void Die()
    {
        if (Target != null) Target.Died -= OnDied;

        Died?.Invoke(this);
    }

    public void HitEnemy()
    {
        if (Target == null)
            return;
        Target.TakeDamage(_damage);
    }

    protected void SetMoveState(Target target)
    {
        if (target == null) return;
        transform.LookAt(new Vector3(Target.transform.position.x, transform.position.y, Target.transform.position.z));
        NavMeshAgent.enabled = true;
        //NavMeshAgent.SetDestination(target.transform.position);
        _animator.PlayRun();
    }

    protected void OnDied(Target unit)
    {
        IsAttack = false;
        unit.Died -= OnDied;
        Target = null;
        LevelObserver.RemoveUnit(unit);
    } //мы не отписываемся тем кем побеждали

    private void ChangeTarget(Target unit)
    {
        if (IsAttack) return;
        if (Target != null) Target.Died -= OnDied;
        Target = unit;
        transform.LookAt(new Vector3(Target.transform.position.x, transform.position.y, Target.transform.position.z));
        Target.Died += OnDied;
    }

    private void TryDie()
    {
        if (Health <= 0)
        {
            NavMeshAgent.enabled = false;
            _isAlive = false;
            Die();
            _bodyCollider.enabled = false;
            _particleSystem.Play();
            _ragdoll.MakePhysics();
        }
    }
}