using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Chaser : MonoBehaviour
{
    //What the chaser moves towards
    [SerializeField] private Transform target;
    //Defines what objects the chaser will chase
    [SerializeField] private string[] targetTagMask;
    //How fast the chaser accelerates towards the target
    [SerializeField] private float acceleration;
    //Max speed before the chaser stops running toward the target
    [SerializeField] private float maxSpeed;

    private Rigidbody2D rb;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (targetTagMask.Contains(other.tag))
        {
            target = other.transform;
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
                if(((target.position - transform.position).x < 0 && transform.localScale.x > 0) || ((target.position - transform.position).x > 0 && transform.localScale.x < 0))
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
