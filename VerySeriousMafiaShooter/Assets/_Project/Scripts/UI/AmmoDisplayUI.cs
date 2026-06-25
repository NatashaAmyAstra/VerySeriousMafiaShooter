using System.Collections.Generic;
using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AmmoDisplayUI : MonoBehaviour
{
    private const string EJECT_BULLET = "EjectBullet";

    private Gun _playerGun;

    [SerializeField] private Transform _bulletHolderTransformTemplate;
    private Stack<Transform> _bulletHolderTransformStack = new();


    private void Awake()
    {
        _bulletHolderTransformTemplate.gameObject.SetActive(false);
    }

    private void Start()
    {
        _playerGun = Player.Instance.GetComponentInChildren<Gun>();

        _playerGun.OnGunFired += OnPlayerGunFired;
        _playerGun.OnGunReloaded += OnPlayerGunReloaded;
    }

    private void OnPlayerGunFired(object sender, EventArgs e)
    {
        EjectBullet();
    }

    private void OnPlayerGunReloaded(object sender, Gun.OnGunReloadEventArgs e)
    {
        float reloadDuration = e.ReloadDuration;
        Sprite bulletImage = e.SelectedBullet.BulletDataSO.UIAmmoSprite;
        int ammoCount = e.AmmoReloaded;

        StartCoroutine(PlayReloadAnimations(reloadDuration, bulletImage, ammoCount));
    }

    private IEnumerator PlayReloadAnimations(float reloadDuration, Sprite bulletImage, int ammoCount)
    {
        while(_bulletHolderTransformStack.Count != 0)
        {
            EjectBullet();
        }

        yield return new WaitForSeconds(reloadDuration);

        for(int i = 0; i < ammoCount; i++)
        {
            Transform newBulletHolder = Instantiate(_bulletHolderTransformTemplate, transform);

            newBulletHolder.GetChild(0).GetComponent<Image>().sprite = bulletImage;
            newBulletHolder.gameObject.SetActive(true);
            _bulletHolderTransformStack.Push(newBulletHolder);
        }
    }


    private void EjectBullet()
    {
        Transform bulletHolder = _bulletHolderTransformStack.Pop();
        bulletHolder.GetChild(0).GetComponent<Animator>().SetTrigger(EJECT_BULLET);
        Destroy(bulletHolder.gameObject, 1);
    }
}
