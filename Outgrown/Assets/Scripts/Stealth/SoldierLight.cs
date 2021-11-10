using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SoldierLight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerStay2D(Collider2D collider)
    {
        print("triggeer");

        if (collider.tag == "Player")
        {
            if (!collider.GetComponent<PlayerCover>().InCover())
            {
                gameObject.GetComponent<Light2D>().color = Color.red;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDeath>().damagePlayer(1);
            }
        }
    }
}
