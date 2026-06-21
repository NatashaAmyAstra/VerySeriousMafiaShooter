using UnityEngine;

public class AIBehaviourStateAggressionBackAway : AIBehaviourStateBase
{
    public override void EnterState(EnemyControllerAI parentController)
    {
        SetBackAwayPosition(parentController);
    }

    public override void Update(EnemyControllerAI parentController)
    {
        RecalculateBackAwayPosition(parentController);
        EvaluateNeedToBackAway(parentController);
    }

    public override void FixedUpdate(EnemyControllerAI parentController)
    {
        // not used
    }

    public override void ExitState(EnemyControllerAI parentController, AIBehaviourStateBase newState)
    {
        parentController.SetState(newState);
    }



    private void SetBackAwayPosition(EnemyControllerAI parentController)
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

        parentController.SetStoppingDistance(0);
        parentController.SetNavMeshDestination(targetPosition);
    }


    private void RecalculateBackAwayPosition(EnemyControllerAI parentController)
    {
        if(parentController.GetDistanceFromPlayer() < parentController.MinimumDistanceFromPlayer)
        {
            SetBackAwayPosition(parentController);
        }
    }


    private void EvaluateNeedToBackAway(EnemyControllerAI parentController)
    {
        if(parentController.HasLineOfSightWithTarget(Player.Instance.position) == false ||
            parentController.GetDistanceFromPlayer() > parentController.MaximumDistanceFromPlayer ||
            parentController.HasReachedDestination())
        {
            ExitState(parentController, parentController.ApproachState);
        }
    }
}
