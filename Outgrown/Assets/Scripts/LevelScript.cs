using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelScript : MonoBehaviour
{
    public AudioClip levelMusic;
    public AudioClip[] randomSounds;
    public float randomSoundInterval;
    public float randomSoundChance;
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayMusic(levelMusic);
        if (randomSounds.Length > 0)
        {
            StartCoroutine(TryPlayRandomSound());
        }
    }

    private IEnumerator TryPlayRandomSound()
    {
        yield return new WaitForSeconds(randomSoundInterval + Random.Range(0, randomSoundInterval));
        if (Random.Range(0f, 1f) < randomSoundChance)
        {
            AudioManager.Instance.PlaySfx(randomSounds[Random.Range(0, randomSounds.Length)]);
        }

        StartCoroutine(TryPlayRandomSound());
    }
}
