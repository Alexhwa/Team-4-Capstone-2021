using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierBehavior : MonoBehaviour
{
    [SerializeField] GameObject DetectionLight;
    [SerializeField] float minAngle = -130;
    [SerializeField] float maxAngle = -90;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartSweep());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StartSweep()
    {
        float rotateAngle = minAngle;
        while (true)
        {
            yield return SweepArea(3, rotateAngle);
            if (rotateAngle == minAngle)
                rotateAngle = maxAngle;
            else
                rotateAngle = minAngle;
        }
    }

    private IEnumerator SweepArea(float duration, float rotateAngle)
    {
        float counter = 0f;
        while (counter < duration)
        {
            if (Time.timeScale == 0)
                counter += Time.unscaledDeltaTime;
            else
                counter += Time.deltaTime;
            /* if (Random.Range(0, 1) < .5f)
            {
                //pos = lookPath[pathIndex];
                pos.x = lookPath[pathIndex].x + Mathf.Sin(counter) * 1f;
                pos.y = lookPath[pathIndex].y + Mathf.Sin(counter) * 1f;
            } */
            // DetectionLight.transform.rotation = Mathf.Lerp(DetectionLight.transform.rotation.z, rotateAngle, Time.deltaTime);
            Quaternion s = Quaternion.Euler(0,0,rotateAngle);
            DetectionLight.transform.rotation = Quaternion.Slerp(DetectionLight.transform.rotation, s, Time.deltaTime);
            yield return null;
        }
    }
}
