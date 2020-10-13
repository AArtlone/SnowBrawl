using System.Collections.Generic;
using UnityEngine;

public static class SoundManager 
{
    public static void PlaySound(Sound sound)
    {
        GameObject soundGameObject = new GameObject("Sound");

        AudioSource audioSource = soundGameObject.AddComponent<AudioSource>();

        SoundAudioClip soundAudioClip = GetAudioClip(sound);

        if (soundAudioClip == null)
            return;

        audioSource.clip = soundAudioClip.audioClip;

        audioSource.volume = soundAudioClip.volume;

        Debug.Log(audioSource.volume);

        audioSource.Play();
    }

    public static void AddButtonSounds(this MyButton myButton)
    {
        myButton.onClick += () => PlaySound(Sound.Button_Click);
    }

    private static SoundAudioClip GetAudioClip(Sound sound)
    {
        foreach (SoundAudioClip soundAudioClip in AudioAssets.Instance.SoundAudioClips)
        {
            if (soundAudioClip.sound == sound)
                return soundAudioClip;
        }

        Debug.LogError("Sound " + sound + " not found");
        return null;
    }
}
