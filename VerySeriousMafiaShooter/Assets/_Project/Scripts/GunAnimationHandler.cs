using System;
using UnityEngine;

public class GunAnimationHandler : MonoBehaviour
{
    private const string FIRE = "Fire";
    private const string RELOAD = "Reload";


    [SerializeField] private AudioClip _audioClipFireBullet;
    [SerializeField] private AudioClip _audioClipReload;


    [SerializeField] private Gun _gun;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        _gun.OnGunFired += PlayFiredAnimation;
        _gun.OnGunReload += PlayReloadAnimation;
    }

    private void PlayFiredAnimation(object sender, EventArgs e)
    {
        _animator.SetTrigger(FIRE);
        AudioPlayer.Instance.PlayClip(_audioClipFireBullet);
    }

    private void PlayReloadAnimation(object sender, EventArgs e)
    {
        _animator.SetTrigger(RELOAD);
        AudioPlayer.Instance.PlayClip(_audioClipReload);
    }
}
