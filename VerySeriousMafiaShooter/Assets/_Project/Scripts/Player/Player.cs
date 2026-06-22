using System;
using UnityEngine;

public class Player : MonoBehaviour, IGunUser
{
    public static Player Instance;


    public event EventHandler OnReloadGun;
    public event IGunUser.BoolReturnEventDelegate OnFireGun;


    public Vector3 position { get { return transform.position; } set { } }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InputManager.Instance.OnPlayerShoot += FireGun;
        InputManager.Instance.OnPlayerReload += Reload;
    }

    private void FireGun(object sender, EventArgs e)
    {
        OnFireGun?.Invoke(this, EventArgs.Empty);
    }

    private void Reload(object sender, EventArgs e)
    {
        OnReloadGun?.Invoke(this, EventArgs.Empty);
    }
}
