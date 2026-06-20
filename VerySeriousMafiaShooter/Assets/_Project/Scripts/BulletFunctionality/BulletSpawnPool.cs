using System.Collections.Generic;
using UnityEngine;

public class BulletSpawnPool : MonoBehaviour
{
    public static BulletSpawnPool Instance;


    [SerializeField] private Transform _bulletPrefab;
    [SerializeField] private int _poolStartSize = 500;
    private Queue<BulletBehaviour> _bulletPool = new();


    private void Awake()
    {
        Instance = this;

        InstantiateInitialPool();
    }

    private void InstantiateInitialPool()
    {
        for(int i = 0; i < _poolStartSize; i++)
        {
            PoolObject(InstantiateBullet());
        }
    }



    public void FireBullet(Vector3 origin, Vector3 fireDirection, BulletDataSO bulletDataSO)
    {
        BulletBehaviour bullet = TakeFromPool();

        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0f, 0f, 90f) * fireDirection);

        float rotationOffset = Random.Range(-bulletDataSO.Inaccuracy, bulletDataSO.Inaccuracy);
        rotation = Quaternion.Euler(0, 0, rotation.eulerAngles.z + rotationOffset);

        bullet.transform.parent = null;
        bullet.transform.position = origin;
        bullet.transform.rotation = rotation;

        bullet.SetBulletDataSO(bulletDataSO);
        bullet.gameObject.SetActive(true);
    }


    public void PoolObject(BulletBehaviour bullet)
    {
        _bulletPool.Enqueue(bullet);
        bullet.transform.parent = transform;
        bullet.gameObject.SetActive(false);
    }


    private BulletBehaviour TakeFromPool()
    {
        if(_bulletPool.TryDequeue(out BulletBehaviour bullet))
        {
            return bullet;
        }

        return InstantiateBullet();
    }

    private BulletBehaviour InstantiateBullet()
    {
        return Instantiate(_bulletPrefab, transform).GetComponent<BulletBehaviour>();
    }
}
