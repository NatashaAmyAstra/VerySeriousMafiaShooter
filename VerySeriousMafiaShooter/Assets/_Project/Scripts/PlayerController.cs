using UnityEngine;
using UnityEngine.InputSystem;
using static InputManager;

[RequireComponent(typeof(Rigidbody2D), typeof(BoxCollider2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float collisionPadding = 0.015f;
    [SerializeField] private LayerMask collisionMask;

    private Rigidbody2D rb;
    private BoxCollider2D myCollider;
    private ContactFilter2D contactFilter;
    private RaycastHit2D[] hitBuffer = new RaycastHit2D[5];

    private Vector2 moveInput;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        myCollider = GetComponent<BoxCollider2D>();

        rb.bodyType = RigidbodyType2D.Kinematic;

        contactFilter.useLayerMask = true;
        contactFilter.SetLayerMask(collisionMask);
        contactFilter.useTriggers = false;
    }


    private void Start()
    {
        // subscribe SetMoveInput to the InputManager singleton's movement event
        InputManager.Instance.OnPlayerMovement += SetMoveInput;
    }

    private void SetMoveInput(object sender, OnPlayerMovementEventArgs args)
    {
        moveInput = args.InputDirection;
    }


    private void FixedUpdate()
    {
        Vector2 velocity = moveInput * speed * Time.fixedDeltaTime;

        Vector2 nextPosition = rb.position;

        nextPosition.x += PlayerMove(new Vector2(velocity.x, 0));
        nextPosition.y += PlayerMove(new Vector2(0, velocity.y));

        rb.MovePosition(nextPosition);
    }

    private float PlayerMove(Vector2 direction)
    {
        float distance = direction.magnitude;
        if (distance < 0.0001f) return 0f;

        int count = myCollider.Cast(direction, contactFilter, hitBuffer, distance + collisionPadding);

        float minDistance = distance;

        for (int i = 0; i < count; i++)
        {
            float hitDistance = hitBuffer[i].distance - collisionPadding;
            if (hitDistance < minDistance)
            {
                minDistance = hitDistance;
            }
        }

        float finalMove = Mathf.Max(0, minDistance);
        return (direction.x != 0) ? (finalMove * Mathf.Sign(direction.x)) : (finalMove * Mathf.Sign(direction.y));
    }
}
