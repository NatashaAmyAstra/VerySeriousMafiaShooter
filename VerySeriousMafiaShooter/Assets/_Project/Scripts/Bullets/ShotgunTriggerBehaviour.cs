using UnityEngine;

public class ShotgunTriggerBehaviour : ITriggerTypeBehaviour
{
    public void Trigger(Vector3 origin, Vector3 fireDirection, BulletDataSO bulletDataSO)
    {
        for(int i = 0; i < bulletDataSO.ProjectileCount; i++)
        {
            BulletSpawnPool.Instance.FireBullet(origin, fireDirection, bulletDataSO);
        }
    }
}
