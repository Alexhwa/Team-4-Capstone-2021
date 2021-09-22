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
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void damagePlayer(float damage)
    {
        if (damage >= playerHealth)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            preloader = GameObject.FindGameObjectWithTag("Preloader").GetComponent<Preloaded>();
            transform.position = preloader.lastCheckpointPos;
        }
        else
        {
            playerHealth -= damage;
        }
    }
}
