using System.Collections;
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
    float maxMoveSpeed = 9.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
	transform.parent.transform.position += movementSpeed * Time.deltaTime;
	if(movementSpeed.x > 0 && movementSpeed.x < maxMoveSpeed) {
            movementSpeed.x += Time.deltaTime * maxMoveSpeed / 2.3f;
	        if(movementSpeed.x > maxMoveSpeed)
		    movementSpeed.x = maxMoveSpeed;
        }
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
            gameObject.GetComponent<AreaEffector2D>().enabled = false;
            //gameObject.SetActive(false);
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
        print("hello");
        if(spriteShown == 0) {
            AudioManager.Instance.PlayMusic(chaseMusic);
        }
        //GameObject[] go = bgsprites.transform.getchil<GameObject>();
	    (bgsprites.transform.GetChild(spriteShown)).gameObject.SetActive(true);
	    spriteShown++;
    }

    [YarnCommand("ChasePlayer")]
    public void chasePlayer() {
        print("mooving");
        movementSpeed = new Vector3(0.01f, 0, 0);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        //gameObject.SetActive(false);
    }
}
