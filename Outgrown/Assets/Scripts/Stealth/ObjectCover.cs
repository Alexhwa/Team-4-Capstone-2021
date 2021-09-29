using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCover : MonoBehaviour
{

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            collider.GetComponent<PlayerCover>().SetInCover(true);
        }
    }
    void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            collider.GetComponent<PlayerCover>().SetInCover(false);
        }
    }
}
