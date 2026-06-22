using System;
using UnityEngine;

public class GunUXHandler : MonoBehaviour
{
    private const string FIRE = "Fire";
    private const string RELOAD = "Reload";


    [SerializeField] private AudioClip _audioClipFireBullet;
    [SerializeField] private AudioClip _audioClipReload;
    [SerializeField] private AudioClip _audioClipJammed;


    [SerializeField] private Gun _gun;
    [SerializeField] private Animator _animator;

    private void Start()
    {
        _gun.OnGunFired += PlayFiredAnimation;
        _gun.OnGunReloaded += PlayReloadAnimation;
        _gun.OnGunJammed += PlayJammedAudio;
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

    private void PlayJammedAudio(object sender, EventArgs e)
    {
        AudioPlayer.Instance.PlayClip(_audioClipJammed);
    }
}
