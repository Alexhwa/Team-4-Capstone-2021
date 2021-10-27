using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class adjustCamera : MonoBehaviour
{
    [SerializeField] Camera cam;
    private DampCamera2D dampCameraScript;
    public float newCameraSize = 19;
    public Vector2 newCameraOffset;
    [Range(0f,1f)]
    public float changeSpeed;
    private bool change;
    
    // Start is called before the first frame update
    void Start()
    {
        dampCameraScript = cam.GetComponent<DampCamera2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (change)
        {
            if (changeSpeed + .02f >= 1)
            {
                change = false;
            }
            changeSpeed = changeSpeed + changeSpeed * changeSpeed;
            cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newCameraSize, changeSpeed);
            dampCameraScript.offset = Vector3.Lerp(dampCameraScript.offset, newCameraOffset, changeSpeed);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            change = true;
        }
    }
}
