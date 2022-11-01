using System;
using UnityEngine;

public class MonsterCollisionHandler : CollisionHandler
{
    public override void OnTriggerEnter(Collider other)
    {
        var nps = other.GetComponent<Npc>();

        if (nps) NpsTaken?.Invoke(nps);
    }

    public event Action<Npc> NpsTaken;
    //private void OnTriggerEnter(Collider other)
    //{
    //    var nps = other.GetComponent<Npc>();
    //    //var mainObstacle = other.GetComponent<MainObstacle>();
    //    //var secondObstacle = other.GetComponent<SecondaryObstacle>();

    //    if (nps) NpsTaken?.Invoke();

    //    //if (mainObstacle) MainObstacle?.Invoke();

    //    //if (secondObstacle) SecondObstacle?.Invoke();
    //}

    ////public event Action NpsTaken;
    ////public event Action MainObstacle;
    ////public event Action SecondObstacle;

}
