using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

public class AudioManager : Singleton<AudioManager>
{
    public static float defaultSFXVolume { get { return .75f; } }

    private Dictionary<AudioAsset, AudioSource> soundSources;
    private AudioSource musicSource;

    public AudioLibrary audioLibrary;

    private void Start()
    {
        soundSources = new Dictionary<AudioAsset, AudioSource>();
        musicSource = gameObject.AddComponent<AudioSource>();
        GenerateSources();
    }

    private void GenerateSources()
    {
        foreach (AudioAsset e in audioLibrary.library)
        {
            var addedSource = gameObject.AddComponent<AudioSource>();
            FillSource(addedSource, e);
            soundSources.Add(e, addedSource);
        }
    }
    
    public void PlayMusic(string clipName)
    {
        AudioAsset sfxAsset = audioLibrary.FindSound(clipName);
        FillSource(musicSource, sfxAsset);
        musicSource.Play();
    }
    public void PlayMusic(AudioClip clip)
    {
        AudioAsset sfxAsset = audioLibrary.FindSound(clip);
        FillSource(musicSource, sfxAsset);
        musicSource.Play();
    }

    /*
     * Call from outside AudioManager to play sound effects
     */

    public void PlaySfx(AudioClip clip)
    {
        AudioAsset sfxAsset = audioLibrary.FindSound(clip);
        AudioSource source = soundSources[sfxAsset];
        FillSource(source, sfxAsset);
        float playVolume = sfxAsset.volume + Random.Range(-sfxAsset.volumeVariance, sfxAsset.volumeVariance);
        if (sfxAsset.overlaps)
        {
            source.PlayOneShot(sfxAsset.clip, playVolume);
        }
        else
        {
            if (!source.isPlaying)
            {
                source.Play();
            }
        }
    }
    public void PlaySfx(string clipName)
    {
        AudioAsset sfxAsset = audioLibrary.FindSound(clipName);
        AudioSource source = soundSources[sfxAsset];
        FillSource(source, sfxAsset);
        float playVolume = sfxAsset.volume + Random.Range(-sfxAsset.volumeVariance, sfxAsset.volumeVariance);
        source.PlayOneShot(sfxAsset.clip, playVolume);
        if (sfxAsset.overlaps)
        {
            source.PlayOneShot(sfxAsset.clip, playVolume);
        }
        else
        {
            if (!source.isPlaying)
            {
                source.Play();
            }
        }
    }

    private void FillSource(AudioSource source, AudioAsset asset)
    {
        source.clip = asset.clip;
        source.loop = asset.loop;
        source.volume = asset.volume + Random.Range(-asset.volumeVariance, asset.volumeVariance);
        source.pitch = asset.pitch + Random.Range(-asset.pitchVariance, asset.pitchVariance);
    }
}
