using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
[RequireComponent(typeof(Rigidbody2D))]
public class Chaser : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private string[] targetTagMask;
    [SerializeField] private float acceleration;
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
                if(((target.position - transform.position).x < 0 && transform.localScale.x > 0) || ((target.position - transform.position).x > 0 && transform.localScale.x < 0))
                {
                    var flipScale = transform.localScale;
                    flipScale.x *= -1;
                    transform.localScale = flipScale;
                }
                rb.velocity += new Vector2((target.position - transform.position).x, 0).normalized * Time.deltaTime *
                               acceleration;
            }
        }
    }
}
