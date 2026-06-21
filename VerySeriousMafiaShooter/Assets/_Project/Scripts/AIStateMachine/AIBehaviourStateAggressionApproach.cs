using UnityEngine;

public class AIBehaviourStateAggressionApproach : AIBehaviourStateBase
{
    public override void EnterState(EnemyControllerAI parentController)
    {
        parentController.SetTarget(Player.Instance.transform);
    }

    public override void Update(EnemyControllerAI parentController)
    {
        MaintainDistance(parentController);
    }

    public override void FixedUpdate(EnemyControllerAI parentController)
    {
        ApproachPlayer(parentController);
    }

    public override void ExitState(EnemyControllerAI parentController, AIBehaviourStateBase newState)
    {
        parentController.SetState(newState);
    }




    private void MaintainDistance(EnemyControllerAI parentController)
    {
        if(parentController.HasLineOfSightWithTarget(Player.Instance.position) == false)
            return;

        if(parentController.GetDistanceFromPlayer() < parentController.MinimumDistanceFromPlayer)
        {
            ExitState(parentController, parentController.BackAwayState);
        }
    }

    private void ApproachPlayer(EnemyControllerAI parentController)
    {
        if(parentController.HasLineOfSightWithTarget(Player.Instance.position) == false)
        {
            parentController.SetStoppingDistance(0);
        }
        else
        {
            parentController.SetStoppingDistance(parentController.MaximumDistanceFromPlayer);
        }

        parentController.SetNavMeshDestination(Player.Instance.position);
    }
}
