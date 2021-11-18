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
        Patrol();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void lookAtObject(Transform trans)
    {
        StopCoroutine(StartSweep());
        StopAllCoroutines();
        StartCoroutine(OnAlert(trans));
    }

    public void Patrol()
    {
        StartCoroutine(StartSweep());
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
            Quaternion s = Quaternion.Euler(0,0,rotateAngle);
            DetectionLight.transform.rotation = Quaternion.Slerp(DetectionLight.transform.rotation, s, Time.deltaTime);
            yield return null;
        }
    }

    private IEnumerator OnAlert(Transform trans)
    {
        float time = 0;
        float duration = 2;

        while (time < duration)
        {
            if (Time.timeScale == 0)
                time += Time.unscaledDeltaTime;
            else
                time += Time.deltaTime;

            Vector3 direction;
            direction = DetectionLight.transform.position - trans.position;
            direction *= -1;
            DetectionLight.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            yield return null;
        }
    }
}
