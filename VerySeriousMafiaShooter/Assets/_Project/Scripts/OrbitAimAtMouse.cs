using UnityEngine;

public class OrbitAimAtMouse : MonoBehaviour
{
    [SerializeField] private Transform _orbitOrigin;
    private float _distanceFromOrigin;

    private void Awake()
    {
        _distanceFromOrigin = Vector2.Distance(transform.position, _orbitOrigin.position);
    }

    private void Update()
    {
        HandleAimPosition();
    }

    private void HandleAimPosition()
    {
        Vector2 origin = _orbitOrigin.position;
        Vector2 targetPosition = Camera.main.ScreenToWorldPoint(InputManager.Instance.MousePosition);
        Vector2 lookVector = (targetPosition - origin).normalized;

        Vector2 lookVectorUp = Quaternion.Euler(0f, 0f, 90f) * lookVector;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, lookVectorUp);

        lookVector *= _distanceFromOrigin;
        transform.position = origin + lookVector;
    }
}
