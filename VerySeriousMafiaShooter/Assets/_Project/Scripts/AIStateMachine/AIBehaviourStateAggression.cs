using UnityEngine;

public class AIBehaviourStateAggression : AIBehaviourStateBase
{
    public override void EnterState(EnemyControllerAI parentController)
    {
        parentController.SetTarget(Player.Instance.transform);
    }

    public override void Update(EnemyControllerAI parentController)
    {
        // not used
    }

    public override void FixedUpdate(EnemyControllerAI parentController)
    {
        HandleMovement(parentController);
    }

    public override void ExitState(EnemyControllerAI parentController, AIBehaviourStateBase newState)
    {
        // not used
    }





    private void HandleMovement(EnemyControllerAI parentController)
    {
        // if the enemy cannot see the player, approach them until they can
        if(parentController.HasLineOfSightWithTarget(Player.Instance.position) == false)
        {
            parentController.SetStoppingDistance(0);
            parentController.SetNavMeshDestination(Player.Instance.position);
            return;
        }

        // if the enemy gets too close to the player, back up a bit
        if(parentController.GetDistanceFromPlayer() < parentController.MinimumDistanceFromPlayer)
        {
            parentController.SetStoppingDistance(0);
            parentController.SetNavMeshDestination(SetBackAwayPosition(parentController));
            return;
        }

        // approach the player
        parentController.SetStoppingDistance(parentController.MaximumDistanceFromPlayer);
        parentController.SetNavMeshDestination(Player.Instance.position);
    }


    private Vector2 SetBackAwayPosition(EnemyControllerAI parentController)
    {
        Vector2 targetDirection = (parentController.transform.position - Player.Instance.position).normalized;
        Vector2 targetPosition = (Vector2)Player.Instance.position + (targetDirection * parentController.MinimumDistanceFromPlayer);

        RaycastHit2D raycastHit = Physics2D.CircleCast(
            parentController.transform.position,
            parentController.CircleCastRadius,
            targetDirection,
            parentController.MaximumDistanceFromPlayer,
            parentController.ObstacleLayerMask);


        if(raycastHit.collider != null)
        {
            targetPosition = raycastHit.point;
        }

        return targetPosition;
    }
}
