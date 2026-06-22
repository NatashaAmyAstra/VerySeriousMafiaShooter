using UnityEngine;

public class EnemyTargetingController : OrbitAimAtTarget
{

    [SerializeField] private Transform _idleTargetTransform;


    protected override void SetDefaultTarget()
    {
        TargetIdleTargetTransform();
    }

    public void SetTarget(Transform target)
    {
        _targetObjectTransform = target;
    }

    public void TargetIdleTargetTransform()
    {
        _targetObjectTransform = _idleTargetTransform;
    }
}
