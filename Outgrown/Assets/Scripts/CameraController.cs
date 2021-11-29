using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float moveSpeed = 3;

    private float offset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
            transform.Translate(Vector3.right * moveSpeed);
        /*
        if (Keyboard.current.aKey.isPressed)
        {
            transform.Translate(Vector3.left * moveSpeed);
        }
        if (Keyboard.current.wKey.isPressed)
        {
            transform.Translate(Vector3.up * moveSpeed);
        }
        if (Keyboard.current.sKey.isPressed)
        {
            transform.Translate(Vector3.down * moveSpeed);
        }
        
        if (Keyboard.current.upArrowKey.isPressed)
        {
            transform.Translate(Vector3.forward * moveSpeed);
        }
        if (Keyboard.current.downArrowKey.isPressed)
        {
            transform.Translate(Vector3.back * moveSpeed);
        }
        */
    }
}
