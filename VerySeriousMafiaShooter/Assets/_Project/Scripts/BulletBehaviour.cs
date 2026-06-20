using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    private BulletDataSO _bulletDataSO;
    private float _speed;
    private float _lifeTime;



    private void Update()
    {
        HandleMovement();
        HandleCulling();
    }



    private void HandleMovement()
    {
        float bulletVelocity = _speed * Time.deltaTime;
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


    private void OnEnable()
    {
        _speed = _bulletDataSO.Speed - Random.Range(0, _bulletDataSO.Variance);
    }

    private void OnDisable()
    {
        _bulletDataSO = null;
    }



    public void SetBulletDataSO(BulletDataSO bulletDataSO)
    {
        _bulletDataSO = bulletDataSO;
    }
}
