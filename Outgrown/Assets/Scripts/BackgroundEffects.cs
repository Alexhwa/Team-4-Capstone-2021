using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundEffects : MonoBehaviour
{
    [SerializeField] Camera camera;

    public int maxY;
    public int minY;
    public int maxX;
    public int minX;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float xpos = Remap(camera.transform.position.x, minX, maxX, -50, 50);
        float ypos = Remap(camera.transform.position.y, minY, maxY, -20, 20);
        gameObject.transform.position = new Vector3(xpos, ypos, 0);
    }

    private float Remap(float value, float from1, float to1, float from2, float to2)
    {
        return (value - from1) / (to1 - from1) * (to2 - from2) + from2;
    }
}
