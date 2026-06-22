using UnityEngine;

public interface ITriggerTypeBehaviour
{
    public void Trigger(Vector3 origin, Vector3 fireDirection, BulletDataSO bulletDataSO);
}
