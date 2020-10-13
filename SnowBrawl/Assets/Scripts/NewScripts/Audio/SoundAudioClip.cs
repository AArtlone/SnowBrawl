using System;
using UnityEngine;

[Serializable]
public class SoundAudioClip
{
    public Sound sound;
    public AudioClip audioClip;
    [Range(0, 1)]
    public float volume;
}
