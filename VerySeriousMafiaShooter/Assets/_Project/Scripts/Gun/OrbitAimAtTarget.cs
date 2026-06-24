using UnityEngine;

public class OrbitAimAtTarget : MonoBehaviour
{
    [SerializeField] private Transform _orbitOriginTransform;
    [SerializeField] private float _lerpSpeed;
    
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
        Vector2 originTransformPosition = _orbitOriginTransform.position;
        Vector2 targetTransformPosition = _targetObjectTransform.position;

        Vector2 targetVector = (targetTransformPosition - originTransformPosition).normalized;
        Vector2 newVector = Vector2.Lerp(transform.localPosition, targetVector, _lerpSpeed).normalized;

        Vector2 lookVectorUp = Quaternion.Euler(0f, 0f, 90f) * newVector;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, lookVectorUp);

        transform.localPosition = (newVector * _distanceFromOrigin);

        float flipScale = Mathf.Sign(newVector.x);
        transform.localScale = new Vector3(1, flipScale, 1);
    }
}
