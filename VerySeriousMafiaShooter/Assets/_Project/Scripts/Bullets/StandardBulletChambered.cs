using UnityEngine;

public class StandardBulletChambered : MonoBehaviour, IChamberedBullet
{
    [SerializeField] private BulletDataSO _bulletDataSO;
    [SerializeField] private Transform _bulletPrefab;

    public void Trigger(Vector3 origin, Vector3 forwardDirection)
    {
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, forwardDirection);
        float rotationOffset = Random.Range(-_bulletDataSO.Inaccuracy, _bulletDataSO.Inaccuracy);

        rotation = Quaternion.Euler(0, 0, rotation.eulerAngles.z + rotationOffset);
        Transform bullet = Instantiate(_bulletPrefab, origin, rotation);
    }
}
