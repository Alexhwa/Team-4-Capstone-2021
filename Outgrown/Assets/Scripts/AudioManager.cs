using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance { get { return _instance ? _instance : _instance = FindObjectOfType<AudioManager>(); } }

    public static float defaultSFXVolume { get { return .75f; } }
    private void Awake()
    {
        //if there already exists a GameState somewhere in the scene, destroy this one
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        //otherwise, store reference
        else
        {
            _instance = this;
        }
    }
    private Dictionary<AudioClip, AudioSource> sfxLibrary = new Dictionary<AudioClip, AudioSource>();
    public AudioSource musicSource;

    private void Start()
    {
        musicSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayMusic(AudioClip clip)
    {
        musicSource.clip = clip;
    }


    /*
     * Call from outside AudioManager to play sound effects
     */

    public void PlaySFX(AudioClip clip)
    {
        AudioSource source = SearchSFX(clip);
        source.PlayOneShot(clip);
    }


    //Overload with volume

    public void PlaySFX(AudioClip clip, float volume)
    {
        AudioSource source = SearchSFX(clip);
        source.volume = volume;
        source.PlayOneShot(clip);
    }


    //Overload with volume and pitch
    public void PlaySFX(AudioClip clip, float volume, float pitch)
    {
        AudioSource source = SearchSFX(clip);
        source.volume = volume;
        source.pitch = pitch;
        source.PlayOneShot(clip);
    }
    
    //Overload with volume and pitch
    public void PlaySFXRandomPitch(AudioClip clip, float minPitch, float maxPitch)
    {
        AudioSource source = SearchSFX(clip);
        source.pitch = Random.Range(minPitch, maxPitch);
        source.PlayOneShot(clip);
    }

    private AudioSource SearchSFX(AudioClip clip)
    {
        AudioSource source = sfxLibrary[clip];
        if (!source)
        {
            sfxLibrary.Add(clip, gameObject.AddComponent<AudioSource>());
            source = sfxLibrary[clip];
        }

        return source;
    }
}
