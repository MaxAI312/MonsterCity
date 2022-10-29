using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Rigidbody[] _allRigidbodies;
    [SerializeField] private Animator _animator;


    public void MakePhysics()
    {
        _animator.enabled = false;
        foreach (var rigidbody in _allRigidbodies)
        {
            rigidbody.isKinematic = false;
            rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        }
    }
}
