using System;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public event EventHandler OnGunFired;
    public event EventHandler OnGunReloaded;
    public event EventHandler OnGunJammed;


    [SerializeField] private Transform _bulletOriginTransform;
    [SerializeField] private int _ammoCountMax;
    private int _ammoCount;

    [SerializeField] private Transform _gunUserTransform;
    private IGunUser _gunUser;

    private List<Bullet> _bullets = new();
    private Bullet _loadedBullet;

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


    private void FireBullet(object sender, EventArgs e)
    {
        if(_ammoCount <= 0)
        {
            OnGunJammed?.Invoke(this, EventArgs.Empty);
            return;
        }

        _loadedBullet.TriggerTypeBehaviour.Trigger(_bulletOriginTransform.position, transform.right, _loadedBullet.BulletDataSO);
        _ammoCount -= 1;

        OnGunFired?.Invoke(this, EventArgs.Empty);
    }

    private void Reload(object sender, EventArgs e)
    {
        if(_bullets.Count == 0 || _ammoCount == _ammoCountMax)
            return;

        int chamberIndex = UnityEngine.Random.Range(0, _bullets.Count);
        _loadedBullet = _bullets[chamberIndex];

        _ammoCount = _ammoCountMax;

        OnGunReloaded?.Invoke(this, EventArgs.Empty);
    }

    public void LoadBullet(Bullet bullet)
    {
        _bullets.Add(bullet);
    }
}
