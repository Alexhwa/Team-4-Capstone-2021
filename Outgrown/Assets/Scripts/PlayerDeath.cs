using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private Preloaded preloader;

    float playerHealth = 1;

    // Start is called before the first frame update
    void Start()
    {
        // preloader = GameObject.FindGameObjectWithTag("Preloader").GetComponent<Preloaded>();
        if (Preloaded.Instance.sceneChange)
        {
            transform.position = Preloaded.Instance.lastCheckpointPos;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void damagePlayer(float damage)
    {
        if (damage >= playerHealth)
        {
            // preloader = GameObject.FindGameObjectWithTag("Preloader").GetComponent<Preloaded>();
            Preloaded.Instance.sceneChange = true;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        else
        {
            playerHealth -= damage;
        }
    }
}
