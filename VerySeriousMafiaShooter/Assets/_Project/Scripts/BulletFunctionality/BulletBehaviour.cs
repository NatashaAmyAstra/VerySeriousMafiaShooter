using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float _velocityVarianceSizeFactor = 30f;
    [SerializeField] private CircleCollider2D _collider;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private LayerMask _layerMask;

    [SerializeField, Range(0, 1)] private float _speedLossOnBounceFactor = 0.2f;
    [SerializeField] private float _angularVelocityOnBounce = 1f;
    [SerializeField] private float _bounceAngleVariation = 5f;
    [SerializeField, Range(0, 1)] private float _bounceAngularVelocityVarianceMinimum = 1f;

    private BulletDataSO _bulletDataSO;
    private float _speed;
    private float _lifeTime;
    private float _lifeTimeVariance;
    private float _velocityVariance;



    private void OnEnable()
    {
        if(_bulletDataSO == null)
            return;

        _velocityVariance = Random.Range(0, _bulletDataSO.VelocityVariance);
        _speed = _bulletDataSO.Velocity - _velocityVariance;
        _lifeTime = 0;
        _lifeTimeVariance = Random.value * 0.1f;

        _rigidbody.bodyType = RigidbodyType2D.Static;
        _collider.enabled = true;
    }

    private void OnDisable()
    {
        _bulletDataSO = null;
    }



    private void Update()
    {
        RaycastHit2D? circleCastHit = HandleCollision();

        HandleMovement();
        HandleCurves();
        HandleCulling();

        HandleRagdoll(circleCastHit);
    }



    public void SetBulletDataSO(BulletDataSO bulletDataSO)
    {
        _bulletDataSO = bulletDataSO;
    }




    private RaycastHit2D? HandleCollision()
    {
        float velocity = _speed * Time.deltaTime;
        RaycastHit2D[] circleCastHitArray = Physics2D.CircleCastAll(transform.position, _collider.radius, transform.right, velocity, _layerMask);

        if(circleCastHitArray.Length == 0)
            return null;

        foreach(RaycastHit2D circleCastHit in circleCastHitArray)
        {
            if(circleCastHit.collider.gameObject.TryGetComponent(out Health health))
            {
                health.Damage(_bulletDataSO.Damage);
                return circleCastHit;
            }
        }

        return circleCastHitArray[0];
    }



    private void HandleMovement()
    {
        if(_rigidbody.bodyType != RigidbodyType2D.Static)
            return;

        float bulletVelocity = _speed * Time.deltaTime;
        Vector3 newPosition = transform.position + transform.right * bulletVelocity;
        transform.position = newPosition;
    }


    private void HandleCurves()
    {
        _speed = _bulletDataSO.Velocity * _bulletDataSO.BulletDragCurve.Evaluate(_lifeTime / _bulletDataSO.LifeTime);
        transform.localScale = Vector3.one * _bulletDataSO.BulletSizeHeightEffectCurve.Evaluate((_lifeTime / _bulletDataSO.LifeTime) + (_velocityVariance / _velocityVarianceSizeFactor));
    }


    private void HandleCulling()
    {
        _lifeTime += Time.deltaTime;

        if(_lifeTime > _bulletDataSO?.LifeTime + _lifeTimeVariance)
        {
            BulletSpawnPool.Instance.PoolObject(this);
        }
    }



    private void HandleRagdoll(RaycastHit2D? hit)
    {
        if(hit == null) 
            return;

        RaycastHit2D circleCastHit = (RaycastHit2D)hit;
        Vector2 newVector = Vector2.Reflect(transform.right, circleCastHit.normal);
        newVector = Quaternion.Euler(0f, 0f, Random.Range(-_bounceAngleVariation, _bounceAngleVariation)) * newVector;
        
        transform.right = newVector;
        transform.position += (Vector3)newVector * _speed * Time.deltaTime;

        _collider.enabled = false;

        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody.linearVelocity = newVector * _speed * _speedLossOnBounceFactor;

        float angularVelocityVariance = Random.Range(_bounceAngularVelocityVarianceMinimum, 1f) * Mathf.Sign(Random.value - 0.5f);
        _rigidbody.angularVelocity = _angularVelocityOnBounce * angularVelocityVariance;
    }

}
