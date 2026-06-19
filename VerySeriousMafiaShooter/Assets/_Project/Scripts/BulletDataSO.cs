using UnityEngine;

[CreateAssetMenu]
public class BulletDataSO : ScriptableObject
{
    public float Speed = 100f;
    public float Variance = 0f;
    public float KillTime = 5f;

    public float Inaccuracy = 0f;
    public int ProjectileCount = 1;
}
