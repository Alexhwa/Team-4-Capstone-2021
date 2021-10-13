using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SingletonPreload : MonoBehaviour
{

    private static SingletonPreload _instance;

    public static SingletonPreload Instance
    {
        get
        {
            if (Application.isEditor)
            {
                if (_instance == null)
                {
                    PlayerPrefs.SetString("Debug Level", SceneManager.GetActiveScene().name);
                    SceneManager.LoadScene("Preload");
                    print("preloading");
                }
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            return _instance;
        }
    }

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
