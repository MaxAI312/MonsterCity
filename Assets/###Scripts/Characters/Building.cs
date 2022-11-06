using System;
using UnityEngine;
using RayFire;

public class Building : Target
{
    [SerializeField] private ParticleSystem _takeDamageEffect;
    [SerializeField] private ParticleSystem _destroyEffect;
    [SerializeField] private float _health;
    
    private RayfireRigid _rayfire;
    private RayfireBomb _bomb;

    private bool _isAlive = true;

    private void Awake()
    {
        _rayfire = GetComponent<RayfireRigid>();
        _bomb = GetComponent<RayfireBomb>();
    }
    
    public override void TakeDamage(float damage)
    {
        if (_isAlive)
        {
            _health -= damage;
            if (_takeDamageEffect != null)
                _takeDamageEffect.Play();
            if (_health <= 0)
            {
                _isAlive = false;
                Died?.Invoke(this);
                Die();
            }
        }
    }

    public override event Action<Target> Died;

    private void Die()
    {
        if (_destroyEffect != null)
        {
            _destroyEffect.transform.parent = null;
            _destroyEffect.Play();
        }
        _rayfire.Demolish();
    
        _bomb.Explode(0);
    }
}