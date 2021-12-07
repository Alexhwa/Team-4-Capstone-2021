using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyKillZone : MonoBehaviour
{
    public GameObject enableObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag.Equals("Bunny"))
        {
            enableObject.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

}
