using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance;


    public Vector3 position { get { return transform.position; } set { } }

    private void Awake()
    {
        Instance = this;
    }
}
