using UnityEngine;
using UnityEngine.InputSystem;

public class TESTGUN : MonoBehaviour
{
    public GameObject BulletToChamber;

    public IChamberedBullet ChamberedBullet;
    public InputAction shootAction;

    private void Awake()
    {
        shootAction.Enable();
        shootAction.performed += Shoot;

        ChamberedBullet = BulletToChamber.GetComponent<IChamberedBullet>();
    }

    private void Shoot(InputAction.CallbackContext value)
    {
        ChamberedBullet.Trigger(transform.position, transform.right);
    }
}
