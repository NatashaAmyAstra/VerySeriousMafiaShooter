using UnityEngine;

[CreateAssetMenu]
public class BulletDataSO : ScriptableObject
{
    public Transform Prefab;

    [Header("Bullet properties")]
    public int Damage = 3;
    public float Velocity = 100f;
    public float VelocityVariance = 0f;
    public float LifeTime = 5f;
    public AnimationCurve BulletDragCurve = AnimationCurve.Linear(0, 1, 1, 0);

    [Header("Gun properties")]
    public float Inaccuracy = 0f;
    public int ProjectileCount = 1;
    public float TriggerCooldown = 0.4f;

    [Header("Game feel modifiers")]
    public AnimationCurve BulletSizeHeightEffectCurve = AnimationCurve.Linear(0, 1, 1, 0.7f);
}
