using System;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class Player : MonoBehaviour, IGunUser
{
    public static Player Instance;


    public event EventHandler OnReloadGun;
    public event IGunUser.GunFireActionEventDelegate OnFireGun;


    public Vector3 position { get { return transform.position; } set { } }


    [SerializeField] private Health _health;



    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        InputManager.Instance.OnPlayerShoot += FireGun;
        InputManager.Instance.OnPlayerReload += Reload;

        _health.OnHealthDepleted += OnPlayerDeath;
    }

    private void FireGun(object sender, EventArgs e)
    {
        OnFireGun?.Invoke(this, EventArgs.Empty);
    }

    private void Reload(object sender, EventArgs e)
    {
        OnReloadGun?.Invoke(this, EventArgs.Empty);
    }


    private void OnPlayerDeath(object sender, EventArgs e)
    {
        Debug.Log("Player died :(");
    }
}
