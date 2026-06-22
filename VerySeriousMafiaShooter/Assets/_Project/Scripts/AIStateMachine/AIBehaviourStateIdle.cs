

public class AIBehaviourStateIdle : AIBehaviourStateBase
{
    public override void EnterState(EnemyControllerAI parentController)
    {
        parentController.SetTarget(null);
    }

    public override void Update(EnemyControllerAI parentController)
    {
        // not used
    }

    public override void FixedUpdate(EnemyControllerAI parentController)
    {
        // check for player in range, then start tracking player
        if(parentController.HasLineOfSightWithTarget(Player.Instance.position))
        {
            ExitState(parentController, parentController.AggressionState);
        }
    }

    public override void ExitState(EnemyControllerAI parentController, AIBehaviourStateBase newState)
    {
        parentController.SetState(newState);
    }
}
