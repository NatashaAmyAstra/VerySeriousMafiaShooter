using System;
using UnityEngine;

public class EnemyAudioHandler : MonoBehaviour
{
    [SerializeField] private EnemyControllerAI _enemyController;
    [SerializeField] private AudioClip _audioClipBecomeAggressive;

    private void Start()
    {
        _enemyController.OnEnemyBecomeAggressive += PlayAgressionClip;
    }

    private void PlayAgressionClip(object sender, EventArgs e)
    {
        AudioPlayer.Instance.PlayClip(_audioClipBecomeAggressive);
    }
}
