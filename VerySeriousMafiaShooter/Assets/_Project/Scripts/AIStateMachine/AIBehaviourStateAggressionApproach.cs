using UnityEngine;

public class AIBehaviourStateAggressionApproach : AIBehaviourStateBase
{
    public override void EnterState(EnemyControllerAI parentController)
    {
        // not used
    }

    public override void Update(EnemyControllerAI parentController)
    {
        AvoidObstacles(parentController);
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




    private void AvoidObstacles(EnemyControllerAI parentController)
    {
        // if the enemy does not have line of sight with the player, approach the player to their exact position to path around obstacles
        if(parentController.HasLineOfSightWithTarget(Player.Instance.transform) == false)
        {
            parentController.SetStoppingDistance(0);
        }
        // once line of sight is regained, continue maintaining follow distance
        else
        {
            parentController.SetStoppingDistance(parentController.MaximumDistanceFromPlayer);
        }
    }

    private void MaintainDistance(EnemyControllerAI parentController)
    {
        if(parentController.GetDistanceFromPlayer() < parentController.MinimumDistanceFromPlayer)
        {
            ExitState(parentController, parentController.BackAwayState);
        }
    }

    private void ApproachPlayer(EnemyControllerAI parentController)
    {
        parentController.SetNavMeshDestination(Player.Instance.transform.position);
    }
}
