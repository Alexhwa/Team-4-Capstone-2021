using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "New Library", menuName = "Audio Library")]
public class AudioLibrary : ScriptableObject
{
    public AudioAsset[] library;

    public AudioAsset FindSound(string clipName)
    {
        foreach (AudioAsset e in library)
        {
            if (e.clipName.Equals(clipName) || e.clip.name.Equals(clipName))
            {
                return e;
            }
        }
        throw new Exception("Asset '" + clipName + "' not found in audio library.");
    }
    
    public AudioAsset FindSound(AudioClip clip)
    {
        foreach (AudioAsset e in library)
        {
            if (clip == e.clip)
            {
                return e;
            }
        }
        throw new Exception("Asset '" + clip.name + "' not found in audio library.");
    }
}

[System.Serializable]
public class AudioAsset
{
    public string clipName;
    public AudioClip clip;
    public bool overlaps = true;
    public bool loop;
    public float volume = 1;
    public float volumeVariance;
    public float pitch = 1;
    public float pitchVariance;
}