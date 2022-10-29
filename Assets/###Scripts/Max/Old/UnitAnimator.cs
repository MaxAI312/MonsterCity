using UnityEngine;

[RequireComponent(typeof(Animator))]

public class UnitAnimator : MonoBehaviour
{
    //private static readonly int Died = Animator.StringToHash(nameof(Died));
    //private static readonly int Speed = Animator.StringToHash(nameof(Speed));
    //private static readonly int Attack = Animator.StringToHash(nameof(Attack));
    private const string Run = nameof(Run);
    private const string Died = nameof(Died);
    private const string Attack = nameof(Attack);
    private const string Idle = nameof(Idle);

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    //public void PlayRun => _animator.SetBool(Run, true);
    //public void Run(float speed) => _animator.SetFloat(Speed, speed);
    public void PlayRun()
    {
        _animator.SetBool(Run, true);
        _animator.SetBool(Attack, false);
        _animator.SetBool(Idle, false);
    }

    public void PlayAttack()
    {
        _animator.SetBool(Run, false);
        _animator.SetBool(Attack, true);
        _animator.SetBool(Idle, false);
    }

    public void PlayIdle()
    {
        _animator.SetBool(Run, false);
        _animator.SetBool(Attack, false);
        _animator.SetBool(Idle, true);
    }    
    
    //public void Die() => _animator.SetTrigger(Died);
    
    //public void LaunchAttack() => _animator.SetBool(Attack, true);

    //public void StopAttack() => _animator.SetBool(Attack, false);
}