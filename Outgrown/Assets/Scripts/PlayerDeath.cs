using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    private Preloaded preloader;
    public Image blackScreen; 
    public float playerHealth = 1;
    private PlayerMovement player;
    private Coroutine screenFadeOut;

    // Start is called before the first frame update
    void Start()
    {
        // preloader = GameObject.FindGameObjectWithTag("Preloader").GetComponent<Preloaded>();
        if (Preloaded.Instance.sceneChange)
        {
            transform.position = CheckpointManager.Instance.lastCheckpointPos;
            Preloaded.Instance.sceneChange = false;
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
            playerHealth = 0;
            if (screenFadeOut == null)
            {
                AudioManager.Instance.PlaySfx("death chord");
                AudioManager.Instance.PlaySfx("damage sound");
                screenFadeOut = StartCoroutine(ChangeScenes());
            }
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
