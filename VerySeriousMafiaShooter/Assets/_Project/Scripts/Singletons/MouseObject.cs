using UnityEngine;

public class MouseObject : MonoBehaviour
{
    public static MouseObject Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        transform.position = (Vector2)Camera.main.ScreenToWorldPoint(InputManager.Instance.MousePosition);
    }
}
