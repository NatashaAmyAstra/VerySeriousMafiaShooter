using UnityEngine;

public class EnemyTargetingController : OrbitAimAtTarget
{

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
        _targetObjectTransform = Player.Instance.transform;
    }
}
