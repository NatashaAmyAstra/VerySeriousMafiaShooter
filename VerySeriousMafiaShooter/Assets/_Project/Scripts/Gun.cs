using System;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public event EventHandler OnGunFired;
    public event EventHandler OnGunReload;


    [SerializeField] private Transform _bulletOriginTransform;
    [SerializeField] private int _ammoCountMax;
    private int _ammoCount;

    private List<Bullet> _bullets = new();
    private Bullet _loadedBullet;


    private void Start()
    {
        InputManager.Instance.OnPlayerShoot += FireBullet;
        InputManager.Instance.OnPlayerReload += Reload;
    }


    private void FireBullet(object sender, EventArgs e)
    {
        if(_ammoCount <= 0)
            return;

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

        OnGunReload?.Invoke(this, EventArgs.Empty);
    }

    public void LoadBullet(Bullet bullet)
    {
        _bullets.Add(bullet);
    }
}
