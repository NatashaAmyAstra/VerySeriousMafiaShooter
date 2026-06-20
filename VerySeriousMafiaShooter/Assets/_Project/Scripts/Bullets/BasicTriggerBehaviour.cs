using UnityEngine;

public class BasicTriggerBehaviour : ITriggerTypeBehaviour
{
    public void Trigger(Vector3 origin, Vector3 fireDirection, BulletDataSO bulletDataSO)
    {
        BulletSpawnPool.Instance.FireBullet(origin, fireDirection, bulletDataSO);
    }
}
