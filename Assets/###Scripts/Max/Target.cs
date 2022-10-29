using System;
using UnityEngine;

public abstract class Target : MonoBehaviour
{
    public abstract void TakeDamage(float damage);
    public abstract event Action<Target> Died;
}