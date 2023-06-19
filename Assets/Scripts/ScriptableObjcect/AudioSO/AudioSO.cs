using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AudioConfiguration", menuName = "ScriptableObjects/AudioConfiguration", order = 1)]
public class AudioSO : ScriptableObject
{
    [HideInInspector] public AudioSource audioSource;
    public AudioClip clip;
    public bool loop;
    public bool playOnAwake;
    [Range(0f, 1f)] public float volume;
    [Range(0f, 1f)] public float pitch;
    public AudioType audioType;

}
