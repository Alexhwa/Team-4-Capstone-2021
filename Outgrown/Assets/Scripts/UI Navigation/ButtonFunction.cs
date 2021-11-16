using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFunction : MonoBehaviour
{
    public void QuitApplication(){
        Application.Quit();
    }

    public void PlayGame(string PlaySceneName)
    {
        SceneManager.LoadScene(PlaySceneName);
    }
}
