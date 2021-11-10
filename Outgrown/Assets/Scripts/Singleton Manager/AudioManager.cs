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

    private void Awake()
    {
        if (audioLibrary == null)
        {
            audioLibrary = Resources.Load<AudioLibrary>("Audio Library");
        }
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
    
    public AudioSource PlayMusic(string clipName)
    {
        AudioAsset sfxAsset = audioLibrary.FindSound(clipName);
        if (sfxAsset.overlaps || !musicSource.isPlaying)
        {
            FillSource(musicSource, sfxAsset);
            musicSource.Play();

            if (sfxAsset.fadeIn)
            {
                StartCoroutine(FadeIn(musicSource, musicSource.volume));
            }
        }

        return musicSource;
    }
    public AudioSource PlayMusic(AudioClip clip)
    {
        AudioAsset sfxAsset = audioLibrary.FindSound(clip);
        if (sfxAsset.overlaps || !musicSource.isPlaying)
        {
            FillSource(musicSource, sfxAsset);
            musicSource.Play();

            if (sfxAsset.fadeIn)
            {
                StartCoroutine(FadeIn(musicSource, musicSource.volume));
            }
        }
        return musicSource;
    }

    /*
     * Call from outside AudioManager to play sound effects
     */

    public AudioSource PlaySfx(AudioClip clip)
    {
        AudioAsset sfxAsset = audioLibrary.FindSound(clip);
        AudioSource source = soundSources[sfxAsset];
        if (sfxAsset.overlaps)
        {
            FillSource(source, sfxAsset);
            source.PlayOneShot(sfxAsset.clip, source.volume);
            if(sfxAsset.fadeIn){ StartCoroutine(FadeIn(source, source.volume)); }
        }
        else
        {
            if (!source.isPlaying)
            {
                FillSource(source, sfxAsset);
                source.Play();
                if(sfxAsset.fadeIn){ StartCoroutine(FadeIn(source, source.volume)); }
            }
        }
        return source;
    }
    public AudioSource PlaySfx(string clipName)
    {
        AudioAsset sfxAsset = audioLibrary.FindSound(clipName);
        AudioSource source = soundSources[sfxAsset];
        if (sfxAsset.overlaps)
        {
            FillSource(source, sfxAsset);
            source.PlayOneShot(sfxAsset.clip, source.volume);
            if(sfxAsset.fadeIn){ StartCoroutine(FadeIn(source, source.volume)); }
        }
        else
        {
            if (!source.isPlaying)
            {
                FillSource(source, sfxAsset);
                source.Play();
                if(sfxAsset.fadeIn){ StartCoroutine(FadeIn(source, source.volume)); }
            }
        }
        return source;
    }

    private void FillSource(AudioSource source, AudioAsset asset)
    {
        source.clip = asset.clip;
        source.loop = asset.loop;
        source.volume = asset.volume + Random.Range(-asset.volumeVariance, asset.volumeVariance);
        source.pitch = asset.pitch + Random.Range(-asset.pitchVariance, asset.pitchVariance);
    }

    private IEnumerator FadeIn(AudioSource source, float endVolume)
    {
        source.volume = 0;
        while (source.volume < endVolume)
        {
            source.volume += Time.deltaTime * .1f;
            yield return null;
        }
    }
}
