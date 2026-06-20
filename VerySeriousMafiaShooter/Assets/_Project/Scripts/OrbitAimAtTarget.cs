using UnityEngine;

public class OrbitAimAtTarget : MonoBehaviour
{
    [SerializeField] private Transform _orbitOriginTransform;
    
    protected Transform _targetObjectTransform;
    private float _distanceFromOrigin;

    protected virtual void Awake()
    {
        _distanceFromOrigin = Vector2.Distance(transform.position, _orbitOriginTransform.position);
    }

    protected virtual void Start()
    {
        SetDefaultTarget();
    }

    protected virtual void Update()
    {
        HandleAimPosition();
    }


    protected virtual void SetDefaultTarget()
    {
        _targetObjectTransform = MouseObject.Instance.transform;
    }


    private void HandleAimPosition()
    {
        Vector2 originPosition = _orbitOriginTransform.position;
        Vector2 targetPosition = _targetObjectTransform.position;
        Vector2 lookVector = (targetPosition - originPosition).normalized;

        Vector2 lookVectorUp = Quaternion.Euler(0f, 0f, 90f) * lookVector;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, lookVectorUp);

        lookVector *= _distanceFromOrigin;
        transform.position = originPosition + lookVector;
    }
}
