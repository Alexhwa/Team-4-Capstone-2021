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
                MonkeyAI monkey = transform.parent.parent.GetComponent<MonkeyAI>();
                monkey.lookAtObject(collider.transform.position);
            }
            else
            {
            }
        }
    }
}
