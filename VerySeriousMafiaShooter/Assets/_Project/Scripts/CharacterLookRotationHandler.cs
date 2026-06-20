using UnityEngine;

public class CharacterLookRotationHandler : MonoBehaviour
{
    private const string BLEND = "Blend";

    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _gunPositionTransform;

    private void Update()
    {
        CalculateRotationState();
    }

    private void CalculateRotationState()
    {
        Vector2 lookVector = _gunPositionTransform.position - transform.position;
        float lookAngle = Vector2.Angle(Vector2.up, lookVector) * Mathf.Sign(lookVector.x);


        int rotationStateValue = Mod(Mathf.RoundToInt((lookAngle / 60)), 6);
        _animator.SetFloat(BLEND, rotationStateValue);
    }

    private int Mod(int a, int n)
    {
        return (a % n + n) % n;
    }
}
