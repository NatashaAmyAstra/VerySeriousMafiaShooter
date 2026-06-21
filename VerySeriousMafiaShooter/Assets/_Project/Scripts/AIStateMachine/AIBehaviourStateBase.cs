using UnityEngine;

public abstract class AIBehaviourStateBase
{
    public abstract void EnterState(EnemyControllerAI parentController);

    public abstract void Update(EnemyControllerAI parentController);

    public abstract void FixedUpdate(EnemyControllerAI parentController);

    public abstract void ExitState(EnemyControllerAI parentController, AIBehaviourStateBase newState);
}
