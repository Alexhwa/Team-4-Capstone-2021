﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
using Yarn.Unity;
using Yarn.Unity.Example;

public class JackboxEndBehavior : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject bgsprites;
    [SerializeField] GameObject dialogueEvent;
    [SerializeField] DialogueRunner dialogueRunner;
    [SerializeField] NPC npc;
    public AudioClip chaseMusic;
    int spriteShown = 0;
    [SerializeField] Vector3 movementSpeed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
	transform.parent.transform.position += movementSpeed * Time.deltaTime;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        print("jacbox boo! (Collision, End)");
        if (collision.transform.tag == "Player")
        {
            animator.SetBool("eventTriggered", true);
  	    dialogueEvent.SetActive(true);
 	    dialogueRunner.StartDialogue(npc.talkToNode);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        print("jacbox boo! (Trigger, End)");
        if (other.tag == "Player")
        {
            animator.SetBool("eventTriggered", true);
            StartCoroutine(waitforseconds());
            gameObject.SetActive(false);
  	    dialogueEvent.SetActive(true);
 	    dialogueRunner.StartDialogue(npc.talkToNode);
        }
    }

    IEnumerator waitforseconds()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2);
    }

    [YarnCommand("ShowSprite")]
    public void ShowSprite() {
	print("Showing sprite " + spriteShown);
        if(spriteShown == 0) {
            AudioManager.Instance.PlayMusic(chaseMusic);
        }
        GameObject[] go = bgsprites.GetComponentsInChildren<GameObject>();
	go[spriteShown].SetActive(true);
	spriteShown++;
    }

    [YarnCommand("ChasePlayer")]
    public void chasePlayer() {
	print("Now chasing player " + spriteShown);
        movementSpeed = new Vector3(10, 0, 0);
    }
}
