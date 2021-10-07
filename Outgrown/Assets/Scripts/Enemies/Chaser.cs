using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
/*
 * Attach to enemy with chasing behavior
 * Script is designed to be able to chase any target and has different possible behavoirs
 *
 * 1. Leave target as null. When a GameObject with a tag included in targetTagMask
 * enters the included trigger hitbox, they are set as the target and the chaser tracks them
 * 
 * 2. Put an object in target and the chaser immediately starts tracking it on start
 */
public class Chaser : MonoBehaviour
{
    //What the chaser moves towards
    [Tooltip("Leave blank to have chaser start tracking when something enters its sight.")]
    [SerializeField] private Transform target;
    //Defines what objects the chaser will chase
    [SerializeField] private string[] targetTagMask;
    //How fast the chaser accelerates towards the target
    [SerializeField] private float acceleration;
    //Max speed before the chaser stops running toward the target
    [SerializeField] private float maxSpeed;
    //How much the chaser slows down when it runs into and destroys an obstacle
    [Range(0f,1f)]
    [SerializeField] private float obstacleDampenFactor;
    
    private Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (targetTagMask.Contains(other.tag))
        {
            target = other.transform;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Equals("Obstacle"))
        {
            Destroy(other.gameObject);
            rb.velocity *= obstacleDampenFactor;
        }

        if (other.gameObject.tag.Equals("Player"))
        {
            GameObject that = other.gameObject;
            var PDscript = that.GetComponent<PlayerDeath>();
            if(PDscript != null) {
                PDscript.damagePlayer(1);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            if (Mathf.Abs(rb.velocity.x) < maxSpeed)
            {
                //flip chaser if target is behind it
                if(((target.position - transform.position).x < 0 && transform.localScale.x > 0) || //turn left
                   ((target.position - transform.position).x > 0 && transform.localScale.x < 0)) // turn right
                {
                    var flipScale = transform.localScale;
                    flipScale.x *= -1;
                    transform.localScale = flipScale;
                }
                //accelerate toward target
                rb.velocity += new Vector2((target.position - transform.position).x, 0).normalized * Time.deltaTime *
                               acceleration;
            }
        }
    }
}
