using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    [SerializeField] private BulletDataSO _bulletDataSO;
    private float _speed;

    private void Awake()
    {
        _speed = _bulletDataSO.Speed - Random.Range(0, _bulletDataSO.Variance);
        Destroy(gameObject, _bulletDataSO.KillTime);
    }

    private void Update()
    {
        float bulletVelocity = _speed * Time.deltaTime;
        Vector3 newPosition = transform.position + transform.up * bulletVelocity;
        transform.position = newPosition;
    }
}
