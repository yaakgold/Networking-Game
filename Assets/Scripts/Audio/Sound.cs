using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public bool loop;

    [Range(.1f, 3f)]
    public float pitch;

    [Range(0f, 1f)]
    public float volume;

    [HideInInspector]
    public AudioSource source;
}