using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preloaded : MonoBehaviour
{
    private static Preloaded instance;

    public Vector3 lastCheckpointPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
            print("destroying");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
