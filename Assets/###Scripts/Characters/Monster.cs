public class Monster : Unit
{
    public override void SetFindTargetState()
    {
        Animator.PlayIdle();
        Target = LevelObserver.FindNearestNpc(this);
        if(Target != null) Target.Died += OnDied;
        SetMoveState(Target);
    }
}