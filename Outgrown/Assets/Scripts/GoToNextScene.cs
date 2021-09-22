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
        if (debugLevel != null)
        {
            SceneManager.LoadScene(debugLevel);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
