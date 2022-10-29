using UnityEngine;

public class Cenobite : Monster
{
    // private float _damage;
    //
    // protected override void FindNewTarget()
    // {
    //     if (LevelObserver.NpcCount < 1)
    //     {
    //         NavMeshAgent.isStopped = true;
    //         Target = null;
    //         return;
    //     }
    //     else 
    //     {
    //         Animator.StopAttack();
    //         NavMeshAgent.isStopped = false;
    //     
    //         Target = LevelObserver.FindNearestNpc(this);
    //         
    //         if (Vector3.Distance(Target.transform.position, transform.position) < 100f)
    //             StartAttack();
    //         // MoveToNewTarget(Target);
    //     }
    // }
    //
    // protected override void TryDestroy()
    // {
    //     if (Health.Amount == 0)
    //     {
    //         Animator.Die();
    //
    //         LevelObserver.RemoveMonster(this);
    //         Destroy(gameObject, 1.5f);
    //     }
    // }
    //
    // public void MakeDamage() //called from animator
    // {
    //     if (Target != null)
    //         Target.TakeDamage(_damage);
    // }
}