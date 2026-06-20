using UnityEngine;

public class CharacterLookRotationHandler : MonoBehaviour
{
    private const string BLEND = "Blend";

    [SerializeField] private Animator _animator;


    [SerializeField] private Transform _targetTransform;

    private void Update()
    {
        CalculateRotationQuadrant(Camera.main.ScreenToWorldPoint(InputManager.Instance.MousePosition));
    }

    private void CalculateRotationQuadrant(Vector2 lookTargetLocation)
    {
        Vector2 lookVector = lookTargetLocation - (Vector2)transform.position;
        float lookAngle = Vector2.Angle(Vector2.up, lookVector) * Mathf.Sign(lookVector.x);


        int rotationStateInt = Mod(Mathf.RoundToInt((lookAngle / 60)), 6);
        _animator.SetFloat(BLEND, rotationStateInt);
    }

    private int Mod(int a, int n)
    {
        return (a % n + n) % n;
    }
}
