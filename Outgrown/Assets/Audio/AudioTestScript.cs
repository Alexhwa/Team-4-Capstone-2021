using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class AudioTestScript : MonoBehaviour
{
    public AudioClip testSound;
    public AudioLibrary library;
    void Start()
    {
        if (library != null)
        {
            print("library is present");
        }
        //ALL SOUNDS MUST BE PUT INTO THE AUDIO LIBRARY ASSET BEFORE THEY CAN BE PLAYED
        //ASSET IS AT Assets/Audio/Audio Library

    }
    
    void Update()
    {
        //Test if sound if findable by sound asset
        if (Keyboard.current.qKey.isPressed)
        {
            AudioManager.Instance.PlaySfx(testSound);
        }
        
        //Test if sound is findable by name
        if (Keyboard.current.eKey.isPressed)
        {
            AudioManager.Instance.PlaySfx("FILL WITH SOUND NAME");
        }
    }
}
