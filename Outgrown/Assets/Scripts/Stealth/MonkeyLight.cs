using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

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
                MonkeyAI monkey = transform.parent.parent.GetComponent<MonkeyAI>();
                monkey.lookAtObject(collider.transform.position);
                transform.GetChild(0).GetComponent<Light2D>().color = Color.red;
                GameObject.FindGameObjectWithTag("Preloader").GetComponent<Preloaded>();
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDeath>().damagePlayer(1);
            }
        }
    }
}
