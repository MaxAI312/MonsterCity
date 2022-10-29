using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollisionHandler : MonoBehaviour
{
    public abstract void OnTriggerEnter(Collider other);
}
