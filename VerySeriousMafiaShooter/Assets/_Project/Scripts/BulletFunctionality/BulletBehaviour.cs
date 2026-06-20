using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private BulletDataSO _bulletDataSO;
    private float _speed;
    private float _lifeTime;




    private void OnEnable()
    {
        _speed = _bulletDataSO.Speed - Random.Range(0, _bulletDataSO.Variance);
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
