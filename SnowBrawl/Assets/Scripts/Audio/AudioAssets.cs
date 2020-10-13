using UnityEngine;

public class AudioAssets : MonoBehaviour
{
    public static AudioAssets Instance;

    [SerializeField] private SoundAudioClip[] soundAudioClips;

    public SoundAudioClip[] SoundAudioClips { get { return soundAudioClips; } }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else
            Instance = this;
    }
}
