using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyLight : MonoBehaviour
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
        if (collider.tag == "Player")
        {
            if (!collider.GetComponent<PlayerCover>().InCover())
            {
                print("Player found, Oo Oo Ah AH");
                collider.GetComponent<SpriteRenderer>().color = Color.red;
                //transform.GetChild(0).GetComponent<Light>().color = Color.red;
            }
            else
            {
                //transform.GetChild(0).GetComponent<Light>().color = Color.white;
                collider.GetComponent<SpriteRenderer>().color = Color.white;
            }
        }
    }
}
