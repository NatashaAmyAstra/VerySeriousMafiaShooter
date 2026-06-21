using UnityEngine;

public class EnemyTargetingController : OrbitAimAtTarget
{

    [SerializeField] private Transform _idleTargetTransform;


    protected override void SetDefaultTarget()
    {
        _targetObjectTransform = _idleTargetTransform;
    }

    public void SetTarget(Transform target)
    {
        _targetObjectTransform = target;
    }
}
