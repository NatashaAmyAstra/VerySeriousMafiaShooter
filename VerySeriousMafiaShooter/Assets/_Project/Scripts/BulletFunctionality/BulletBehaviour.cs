using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float _velocityVarianceSizeFactor = 30f;
    [SerializeField] private Collider2D _collider;
    [SerializeField] private LayerMask _layerMask;

    private BulletDataSO _bulletDataSO;
    private float _speed;
    private float _lifeTime;
    private float _velocityVariance;

    private ContactFilter2D _contactFilter = new();



    private void OnEnable()
    {
        if(_bulletDataSO == null)
            return;

        _velocityVariance = Random.Range(0, _bulletDataSO.VelocityVariance);
        _speed = _bulletDataSO.Velocity - _velocityVariance;
        _lifeTime = 0;

        _contactFilter.useLayerMask = true;
        _contactFilter.layerMask = _layerMask;
    }

    private void OnDisable()
    {
        _bulletDataSO = null;
    }



    private void Update()
    {
        HandleMovement();
        HandleCollision();
        HandleCulling();
    }



    private void HandleCollision()
    {
        List<Collider2D> results = new();
        _collider.Overlap(_contactFilter, results);

        foreach(Collider2D collider in results)
        {
            if(collider.gameObject.TryGetComponent(out Health health))
            {
                health.Damage(_bulletDataSO.Damage);
            }
        }
    }

    private void HandleMovement()
    {
        float dragMultiplier = _bulletDataSO.BulletDragCurve.Evaluate(_lifeTime / _bulletDataSO.LifeTime);
        float bulletVelocity = _speed * Time.deltaTime * dragMultiplier;
        Vector3 newPosition = transform.position + transform.right * bulletVelocity;
        transform.position = newPosition;

        transform.localScale = Vector3.one * _bulletDataSO.BulletSizeHeightEffectCurve.Evaluate((_lifeTime / _bulletDataSO.LifeTime) + (_velocityVariance / _velocityVarianceSizeFactor));
    }

    private void HandleCulling()
    {
        _lifeTime += Time.deltaTime;

        if(_lifeTime > _bulletDataSO.LifeTime)
        {
            BulletSpawnPool.Instance.PoolObject(this);
        }
    }


    public void SetBulletDataSO(BulletDataSO bulletDataSO)
    {
        _bulletDataSO = bulletDataSO;
    }
}
