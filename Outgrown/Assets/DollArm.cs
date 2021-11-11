using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollArm : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Transform torso = collision.transform.GetChild(3).GetChild(4).GetChild(0); // Get torso bone of player
            transform.SetParent(torso);
            transform.localPosition = new Vector3(0, 0, 0);
            transform.rotation.eulerAngles.Set(0, 0, -45);
        }
        if (collision.name == "DollSprite_0")
        {

        }
    }
}
