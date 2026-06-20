using UnityEngine;
using UnityEngine.InputSystem;

public class TESTGUN : MonoBehaviour
{
    public GameObject BulletToChamber;

    public BasicTriggerBehaviour ChamberedBullet;
    public InputAction shootAction;

    private void Awake()
    {
        shootAction.Enable();
        shootAction.performed += Shoot;

        ChamberedBullet = BulletToChamber.GetComponent<BasicTriggerBehaviour>();
    }

    private void Shoot(InputAction.CallbackContext value)
    {
        
    }
}
