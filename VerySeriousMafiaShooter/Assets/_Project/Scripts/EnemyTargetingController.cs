using UnityEngine;

public class EnemyTargetingController : OrbitAimAtTarget
{
    protected override void SetDefaultTarget()
    {
        _targetObjectTransform = Player.Instance.transform;
    }
}
