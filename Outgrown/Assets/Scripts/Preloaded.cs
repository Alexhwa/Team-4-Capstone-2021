using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Preloaded : MonoBehaviour
{
    public Vector3 lastCheckpointPos;
    public bool sceneChange = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
