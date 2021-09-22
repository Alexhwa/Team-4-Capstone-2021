using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyAI : MonoBehaviour
{
    [SerializeField] GameObject left_eye;
    [SerializeField] GameObject right_eye;
    [SerializeField] GameObject player;
    [SerializeField] GameObject monkeyPath;

    public float rotate_speed = 5;

    private List<Vector3> lookPath;
    private int pathIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        lookPath = new List<Vector3>();
        for (int i = 0; i < monkeyPath.transform.childCount; i++)
        {
            lookPath.Add(monkeyPath.transform.GetChild(i).position);
        }
        StartCoroutine("scanner");
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void lookAtObject(Vector3 pos)
    {
        StopCoroutine("scanner");
        pos[2] = 0;     // ensure we are in 2D
        Vector3 direction;
        direction = left_eye.transform.position - pos;
        left_eye.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        direction = right_eye.transform.position - pos;
        right_eye.transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    }

    private IEnumerator scanner()
    {
        bool forward = true;
        while (true)
        {
            if (forward)
                pathIndex += 1;
            else
                pathIndex -= 1;
            if (pathIndex >= lookPath.Count)
            {
                forward = false;
                pathIndex -= 2;
            }
            if (pathIndex < 0)
            {
                forward = true;
                pathIndex += 2;
            }
            // yield return new WaitForSeconds(6);
            yield return rotateOverTime(5);
        }
    }

    private IEnumerator rotateOverTime(float duration)
    {
        Vector3 pos = lookPath[pathIndex];
        float counter = 0f;
        while (counter < duration)
        {
            if (Time.timeScale == 0)
                counter += Time.unscaledDeltaTime;
            else
                counter += Time.deltaTime;
            if (Random.Range(0, 1) < .5f)
            {
                //pos = lookPath[pathIndex];
                pos.x = lookPath[pathIndex].x + Mathf.Sin(counter) * 1f;
                pos.y = lookPath[pathIndex].y + Mathf.Sin(counter) * 1f;
            }
            /*float angle = 45;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            left_eye.transform.rotation = Quaternion.Slerp(left_eye.transform.rotation, rotation, counter / duration);
            right_eye.transform.rotation = Quaternion.Slerp(right_eye.transform.rotation, rotation, counter / duration);*/

            Vector3 direction;
            direction = left_eye.transform.position - pos;
            Quaternion leftRot = Quaternion.LookRotation(Vector3.forward, direction);
            direction = right_eye.transform.position - pos;
            Quaternion rightRot = Quaternion.LookRotation(Vector3.forward, direction);
            left_eye.transform.rotation = Quaternion.Slerp(left_eye.transform.rotation, leftRot, counter / duration);
            right_eye.transform.rotation = Quaternion.Slerp(right_eye.transform.rotation, rightRot, counter / duration);
            yield return null;
        }
    }
}
