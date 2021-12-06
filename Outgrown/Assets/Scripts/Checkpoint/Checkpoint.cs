using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public AudioClip musicChange;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            CheckpointManager.Instance.setCheckpoint(this);
            print("triggered checkpoint");
            
            if(musicChange)
            {
                AudioManager.Instance.PlayMusic(musicChange);
            }
        }
    }
}
