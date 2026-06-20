using UnityEngine;

public class CharacterLookRotationHandler : MonoBehaviour
{
    private const string ROTATION_STATE = "rotation_state";

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
        _animator.SetInteger(ROTATION_STATE, rotationStateInt);
    }

    private int Mod(int a, int n)
    {
        return (a % n + n) % n;
    }
}
