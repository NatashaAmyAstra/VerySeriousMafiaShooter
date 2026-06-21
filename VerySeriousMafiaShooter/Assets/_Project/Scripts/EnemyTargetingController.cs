using UnityEngine;

public class EnemyTargetingController : OrbitAimAtTarget
{

    [SerializeField] private Transform _idleTargetTransform;

    protected override void Awake()
    {
        base.Awake();
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }


    protected override void SetDefaultTarget()
    {
        _targetObjectTransform = _idleTargetTransform;
    }
}
