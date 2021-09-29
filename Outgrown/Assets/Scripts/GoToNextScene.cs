using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToNextScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string debugLevel = PlayerPrefs.GetString("Debug Level");
        if (debugLevel != "" && debugLevel != "Preload")
        {
            SceneManager.LoadScene(debugLevel);
            PlayerPrefs.SetString("Debug Level", null);
        }
        else
        {
            print("load first scene");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
