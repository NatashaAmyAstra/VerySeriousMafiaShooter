using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public static AudioPlayer Instance;


    [SerializeField] private AudioSource _audioSource;

    private void Awake()
    {
        Instance = this;
    }

    public void PlayClip(AudioClip clip)
    {
        _audioSource.PlayOneShot(clip);
    }
}
