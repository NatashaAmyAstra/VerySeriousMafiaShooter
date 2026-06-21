using UnityEngine;

[CreateAssetMenu]
public class BulletDataSO : ScriptableObject
{
    public Transform Prefab;

    public float Velocity = 100f;
    public float VelocityVariance = 0f;
    public float KillTime = 5f;

    public float Inaccuracy = 0f;
    public int ProjectileCount = 1;

    public AnimationCurve BulletDragCurve = AnimationCurve.Linear(0, 1, 1, 0);
    public AnimationCurve BulletSizeHeightEffectCurve = AnimationCurve.Linear(0, 1, 1, 0.7f);
}
