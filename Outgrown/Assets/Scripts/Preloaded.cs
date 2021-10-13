using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class Preloaded : Singleton<Preloaded>
{
    public Vector3 lastCheckpointPos;
    public bool sceneChange = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        /*if (Keyboard.current.rKey.isPressed)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }*/
    }
}
