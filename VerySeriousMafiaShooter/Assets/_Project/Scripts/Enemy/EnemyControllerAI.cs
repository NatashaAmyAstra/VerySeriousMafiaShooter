using System;
using UnityEngine;
using UnityEngine.AI;

public class EnemyControllerAI : MonoBehaviour, IGunUser
{
    private enum aggressionState
    {
        Idle,
        Aggression,
        AggressionBackAway
    }
    
    public enum weaponTarget
    {
        Player,
        Idle
    }

    private AIBehaviourStateBase _behaviourState;

    public AIBehaviourStateIdle IdleState = new AIBehaviourStateIdle();
    public AIBehaviourStateAggression AggressionState = new AIBehaviourStateAggression();


    public event EventHandler OnEnemyBecomeAggressive;
    public event EventHandler OnReloadGun;
    public event IGunUser.GunFireActionEventDelegate OnFireGun;

    [Header("Behaviour")]
    [SerializeField] private float _detectPlayerRange = 10f;
    [SerializeField] private float _timeBetweenShootAttempts = 0.5f;

    [SerializeField] private float _idleTargetChangeTimerMin = 1f;
    [SerializeField] private float _idleTargetChangeTimerMax = 8f;

    // fields exposed through properties
    [SerializeField] private float _distanceFromPlayerMax = 5f;
    [SerializeField] private float _distanceFromPlayerMin = 1.5f;


    [Header("Obstacle avoidance")]
    [SerializeField] private float _circleCastRadius = 0.2f;
    [SerializeField] private LayerMask _obstacleLayerMask;
   


    public float DistanceFromPlayerMax { get { return _distanceFromPlayerMax; } set { } }
    public float DistanceFromPlayerMin { get { return _distanceFromPlayerMin; } set { } }

    public float CircleCastRadius { get { return _circleCastRadius; } set { } }
    public LayerMask ObstacleLayerMask { get { return _obstacleLayerMask; } set { } }



    [Header("Debug")]
    [SerializeField] private aggressionState _defaultAIBehaviourState = aggressionState.Idle;

    [Header("References")]
    [SerializeField] private NavMeshAgent _navMeshAgent;
    [SerializeField] private EnemyTargetingController _enemyTargetingController;
    [SerializeField] private Transform _idleTargetTransform;


    // private fields
    private float _idleTargetTimer;
    private float _fireAttemptCooldown;


    private void Awake()
    {
        _navMeshAgent.updateRotation = false;
        _navMeshAgent.updateUpAxis = false;


        // set behaviour state as selected in editor. Intended to be idle, but can be changed for debugging purposes
        switch(_defaultAIBehaviourState)
        {
            case (aggressionState.Idle):
                SetState(IdleState);
                break;
            case (aggressionState.Aggression):
                SetState(AggressionState);
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
        if(newState == AggressionState)
            OnEnemyBecomeAggressive?.Invoke(this, EventArgs.Empty);

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
    public bool HasLineOfSightWithTarget(Vector3 targetPosition)
    {
        if(Vector2.Distance(targetPosition, transform.position) > _detectPlayerRange)
            return false;

        Vector2 circleCastDirection = targetPosition - transform.position;
        RaycastHit2D raycastHit = Physics2D.CircleCast(transform.position, _circleCastRadius, circleCastDirection, circleCastDirection.magnitude, _obstacleLayerMask);
        return raycastHit.collider == null;
    }

    public float GetDistanceFromPlayer()
    {
        return Vector3.Distance(transform.position, Player.Instance.position);
    }

    public bool HasReachedDestination()
    {
        return transform.position == _navMeshAgent.destination;
    }

    public void SetTarget(Transform target)
    {
        if(target != null)
        {
            _enemyTargetingController.SetTarget(target);
            return;
        }

        _enemyTargetingController.TargetIdleTargetTransform();
    }

    public Gun.fireActionResult TryFireGun()
    {
        _fireAttemptCooldown -= Time.deltaTime;

        if(_fireAttemptCooldown > 0)
            return Gun.fireActionResult.failed;

        Gun.fireActionResult? returnValue = OnFireGun?.Invoke(this, EventArgs.Empty);
        _fireAttemptCooldown = _timeBetweenShootAttempts;

        if(returnValue == null)
            return Gun.fireActionResult.failed;

        return (Gun.fireActionResult)returnValue;
    }

    public void Reload()
    {
        OnReloadGun?.Invoke(this, EventArgs.Empty);
    }


    public void AnimateIdleTargetPosition()
    {
        _idleTargetTimer -= Time.deltaTime;

        if(_idleTargetTimer > 0)
            return;

        
        _idleTargetTimer = UnityEngine.Random.Range(_idleTargetChangeTimerMin, _idleTargetChangeTimerMax);
        _idleTargetTransform.localPosition = UnityEngine.Random.insideUnitCircle.normalized;
    }
}
