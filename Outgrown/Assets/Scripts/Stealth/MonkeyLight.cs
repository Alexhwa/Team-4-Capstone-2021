using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class MonkeyLight : MonoBehaviour
{
    [SerializeField] AudioClip monkey_scream;
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
                AudioManager.Instance.PlaySfx(monkey_scream);
                MonkeyAI monkey = transform.parent.parent.GetComponent<MonkeyAI>();
                monkey.lookAtObject(collider.transform.position);
                transform.GetChild(0).GetComponent<Light2D>().color = Color.red;
                GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDeath>().damagePlayer(1);
            }
        }
    }
}
