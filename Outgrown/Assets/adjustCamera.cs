﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class adjustCamera : MonoBehaviour
{
    [SerializeField] Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            cam.orthographicSize = 20.0f;
            cam.GetComponent<DampCamera2D>().offset.y = 5;
        }
    }
}
