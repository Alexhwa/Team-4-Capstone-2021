using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackboxBehavior : MonoBehaviour
{
    [SerializeField] Animator animator;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        print("jacbox boo!");
        if (collision.transform.tag == "Player")
        {
            animator.SetBool("eventTriggered", true);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        print("jacbox boo!");
        if (other.tag == "Player")
        {

            animator.SetBool("eventTriggered", true);
            StartCoroutine(waitforseconds());
            gameObject.SetActive(false);
        }
    }

    IEnumerator waitforseconds()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(2);
    }
}