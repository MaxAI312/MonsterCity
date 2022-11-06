using System;
using UnityEngine;

public class NpcRadar : MonoBehaviour
{
    [SerializeField] private Npc _npc;

    private void OnTriggerStay(Collider other)
    {
        if (_npc.Target == null)
        {
            var monster = other.GetComponent<Monster>();

            if (monster)
                MonsterTaken?.Invoke(monster);
        }
    }

    public event Action<Monster> MonsterTaken;
}