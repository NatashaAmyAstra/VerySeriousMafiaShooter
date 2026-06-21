using UnityEngine;
using UnityEngine.AI;

public class EnemyControllerAI : MonoBehaviour
{
    private enum behaviourState
    {
        Idle,
        AggressionApproach,
        AggressionBackAway
    }


    private AIBehaviourStateBase _behaviourState;

    public AIBehaviourStateIdle IdleState = new AIBehaviourStateIdle();
    public AIBehaviourStateAggressionApproach ApproachState = new AIBehaviourStateAggressionApproach();
    public AIBehaviourStateAggressionBackAway BackAwayState = new AIBehaviourStateAggressionBackAway();




    [SerializeField] private behaviourState _defaultAIBehaviourState = behaviourState.Idle;
    [SerializeField] private NavMeshAgent _navMeshAgent;

    
    [SerializeField] private float _maximumDistanceFromPlayer = 5f;
    public float MaximumDistanceFromPlayer { get { return _maximumDistanceFromPlayer; } set { } }
    
    [SerializeField] private float _minimumDistanceFromPlayer = 3f;
    public float MinimumDistanceFromPlayer { get { return _minimumDistanceFromPlayer; } set { } }

    [SerializeField] private float _circleCastRadius = 0.2f;
    public float CircleCastRadius { get { return _circleCastRadius; } set { } }

    [SerializeField] private LayerMask _obstacleLayerMask;
    public LayerMask ObstacleLayerMask { get { return _obstacleLayerMask; } set { } }

    
    private Transform _navMeshTargetTransform;

    private void Awake()
    {
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;


        // set behaviour state as selected in editor. Intended to be idle, but can be changed for debugging purposes
        switch(_defaultAIBehaviourState)
        {
            case (behaviourState.Idle):
                SetState(IdleState);
                break;
            case (behaviourState.AggressionApproach):
                SetState(ApproachState);
                break;
            case (behaviourState.AggressionBackAway):
                SetState(BackAwayState);
                break;
        }
    }



    private void Update()
    {
        _behaviourState.Update(this);
    }

    private void FixedUpdate()
    {
        _behaviourState.FixedUpdate(this);
    }


    // controller methods
    public void SetState(AIBehaviourStateBase newState)
    {
        _behaviourState = newState;
        newState.EnterState(this);
    }

    public void SetStoppingDistance(float distance)
    {
        _navMeshAgent.stoppingDistance = distance;
    }

    public void SetNavMeshDestination(Vector3 navMeshDestination)
    {
        _navMeshAgent.SetDestination(navMeshDestination);
    }


    // helper methods
    public bool HasLineOfSightWithTarget(Transform targetTransform)
    {
        Vector2 circleCastDirection = targetTransform.position - transform.position;
        RaycastHit2D raycastHit = Physics2D.CircleCast(transform.position, _circleCastRadius, circleCastDirection, circleCastDirection.magnitude, _obstacleLayerMask);
        return raycastHit.collider == null;
    }

    public float GetDistanceFromPlayer()
    {
        return Vector3.Distance(transform.position, Player.Instance.transform.position);
    }

    public bool HasReachedDestination()
    {
        return transform.position == _navMeshAgent.destination;
    }
}
