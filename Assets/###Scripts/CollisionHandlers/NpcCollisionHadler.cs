using System;
using UnityEngine;

public class NpcCollisionHadler : CollisionHandler
{
    public override void OnTriggerEnter(Collider other)
    {
        var monster = other.GetComponent<Monster>();

        if (monster) MonsterTaken?.Invoke(monster);
    }

    public event Action<Monster> MonsterTaken;


    //public override void OnTriggerEnter(Collider other)
    //{
    //    var monster = other.GetComponent<Monster>();

    //    if (monster) MonsterTaken?.Invoke(); 

    //}
}
