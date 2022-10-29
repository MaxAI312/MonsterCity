using System;
using UnityEngine;

//[RequireComponent(typeof(CapsuleCollider))]

public class NpcRadar : MonoBehaviour
{
    [SerializeField] private Npc _npc;
    //public event Action<Collider> EnterDetected;
    //public event Action<Collider> ExitDetected;

    //private void OnValidate()
    //{
    //    GetComponent<CapsuleCollider>().isTrigger = true;
    //}

    //private void OnTriggerEnter(Collider collider) => EnterDetected?.Invoke(collider);
    //private void OnTriggerExit(Collider collider) => ExitDetected?.Invoke(collider);

    private void OnTriggerStay(Collider other)
    {
        // if(_npc.IsAttack)
        //     return;
        
        //Debug.Log("OnTriggerEnter");

        if (_npc.Target == null)
        {
            var monster = other.GetComponent<Monster>();

            if (monster)
            {
                //Debug.Log("ChangeTarget");
                MonsterTaken?.Invoke(monster);
            }
        }
    }

    public event Action<Monster> MonsterTaken;
}

