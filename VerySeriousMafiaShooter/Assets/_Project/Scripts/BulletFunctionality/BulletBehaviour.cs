using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private float _velocityVarianceSizeFactor = 30f;

    private BulletDataSO _bulletDataSO;
    private float _speed;
    private float _lifeTime;

    private float _velocityVariance;



    private void OnEnable()
    {
        _velocityVariance = Random.Range(0, _bulletDataSO.VelocityVariance);
        _speed = _bulletDataSO.Velocity - _velocityVariance;
        _lifeTime = 0;
    }

    private void OnDisable()
    {
        _bulletDataSO = null;
    }



    private void Update()
    {
        HandleMovement();
        HandleCulling();
    }



    private void HandleMovement()
    {
        float dragMultiplier = _bulletDataSO.BulletDragCurve.Evaluate(_lifeTime / _bulletDataSO.KillTime);
        float bulletVelocity = _speed * Time.deltaTime * dragMultiplier;
        Vector3 newPosition = transform.position + transform.right * bulletVelocity;
        transform.position = newPosition;

        transform.localScale = Vector3.one * _bulletDataSO.BulletSizeHeightEffectCurve.Evaluate((_lifeTime / _bulletDataSO.KillTime) + (_velocityVariance / _velocityVarianceSizeFactor));
    }

    private void HandleCulling()
    {
        _lifeTime += Time.deltaTime;

        if(_lifeTime > _bulletDataSO.KillTime)
        {
            BulletSpawnPool.Instance.PoolObject(this);
        }
    }


    public void SetBulletDataSO(BulletDataSO bulletDataSO)
    {
        _bulletDataSO = bulletDataSO;
    }
}
