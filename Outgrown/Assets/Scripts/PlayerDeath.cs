using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private Preloaded preloader;
    public Image blackScreen; 
    float playerHealth = 1;
    private PlayerMovement player;

    // Start is called before the first frame update
    void Start()
    {
        // preloader = GameObject.FindGameObjectWithTag("Preloader").GetComponent<Preloaded>();
        if (Preloaded.Instance.sceneChange)
        {
            transform.position = CheckpointManager.Instance.lastCheckpointPos;
        }

        player = GetComponent<PlayerMovement>();
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
            StartCoroutine(ChangeScenes());
        }
        else
        {
            playerHealth -= damage;
        }
    }

    private IEnumerator ChangeScenes()
    {
        player.anim.Play("PlayerDeathEnvironment");
        blackScreen.enabled = true;
        while (blackScreen.color.a < 1)
        {
            Color newCol = blackScreen.color;
            newCol.a += Time.deltaTime;
            blackScreen.color = newCol;
            yield return null;
        }
        Preloaded.Instance.sceneChange = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
