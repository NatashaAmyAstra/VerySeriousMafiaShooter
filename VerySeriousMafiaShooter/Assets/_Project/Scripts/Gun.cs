using System;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public enum fireActionResult
    {
        failed = -1,
        fired,
        noAmmo,
        notReady
    }


    public event EventHandler OnGunFired;
    public event EventHandler OnGunReloaded;
    public event EventHandler OnEmptyGunFired;


    [SerializeField] private Transform _bulletOriginTransform;
    [SerializeField] private float _reloadDuration = 0.5f;
    [SerializeField] private int _ammoCountMax;
    private int _ammoCount;

    [SerializeField] private Transform _gunUserTransform;
    private IGunUser _gunUser;

    private List<Bullet> _bullets = new();
    private Bullet _loadedBullet;

    private float _triggerCooldown = 0f;



    private void Awake()
    {
        if(_gunUserTransform.TryGetComponent(out IGunUser gunUser))
        {
            _gunUser = gunUser;
        }
        else
        {
            throw new Exception("Reference placed in \"_gunUserTransform\" must contain a component of type \"IGunUser\"");
        }
    }

    private void Start()
    {
        _gunUser.OnFireGun += FireBullet;
        _gunUser.OnReloadGun += Reload;
    }


    private void Update()
    {
        _triggerCooldown -= Time.deltaTime;
    }


    private fireActionResult FireBullet(object sender, EventArgs e)
    {
        // can't fire until gun is ready since last shot
        if(_triggerCooldown > 0f)
        {
            return fireActionResult.notReady;
        }

        // can't fire if no ammo... duh
        if(_ammoCount <= 0)
        {
            OnEmptyGunFired?.Invoke(this, EventArgs.Empty);
            return fireActionResult.noAmmo;
        }

        // trigger bullet firing behaviour
        _loadedBullet.TriggerTypeBehaviour.Trigger(_bulletOriginTransform.position, transform.right, _loadedBullet.BulletDataSO);

        // update gun data and trigger related events
        _ammoCount -= 1;
        _triggerCooldown = _loadedBullet.BulletDataSO.TriggerCooldown;
        OnGunFired?.Invoke(this, EventArgs.Empty);

        return fireActionResult.fired;
    }

    private void Reload(object sender, EventArgs e)
    {
        // can't reload if there's no bullets to reload, nor if the gun is already fully loaded
        if(_bullets.Count == 0 || _ammoCount == _ammoCountMax)
        {
            Debug.Log(_bullets.Count);
            return;
        }

        int chamberIndex = UnityEngine.Random.Range(0, _bullets.Count);
        _loadedBullet = _bullets[chamberIndex];

        _ammoCount = _ammoCountMax;
        _triggerCooldown = _reloadDuration;

        OnGunReloaded?.Invoke(this, EventArgs.Empty);
    }

    public void LoadBullet(Bullet bullet)
    {
        _bullets.Add(bullet);
    }
}
