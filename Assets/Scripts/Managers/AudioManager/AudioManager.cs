using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum AudioType
{
    CollectSound,
    DropSound
}
public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSO[] sounds;

    private void Awake()
    {
        SoundConfiguration();
      
    }
    private void OnEnable()
    {
        GemManager.OnGemDropSound += PlaySound;
        GemInteraction.OnPlayerCollectGemSound += PlaySound;
    }
    private void Start()
    {
       // PlaySound("BgMusic", true);
    }

    private void SoundConfiguration()
    {
        foreach (var sound in sounds)
        {
            sound.audioSource = gameObject.AddComponent<AudioSource>();
            sound.audioSource.clip = sound.clip;
            sound.audioSource.volume = sound.volume;
            sound.audioSource.playOnAwake = sound.playOnAwake;
            sound.audioSource.loop = sound.loop;
        }
    }
    public void PlaySound(AudioType audioType, bool state)
    {
        AudioSO audio = Array.Find(sounds, sound => sound.audioType == audioType);

        if (state)
        {
            if (audio == null)
                return;
            audio.audioSource.Play();
        }
        else
            audio.audioSource.Stop();

    }
    private void OnDisable()
    {
        GemInteraction.OnPlayerCollectGemSound -= PlaySound;
        GemManager.OnGemDropSound -= PlaySound;

    }
}
