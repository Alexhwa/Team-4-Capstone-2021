using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyAI : MonoBehaviour
{
    [SerializeField] GameObject left_eye;
    [SerializeField] GameObject right_eye;

    public float rotate_speed = 5;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(scanner());
    }

    // Update is called once per frame
    void Update()
    {
    }

    private IEnumerator scanner()
    {
        float angle1 = -45;
        float angle2 = 45;
        int count = 0;
        while (true)
        {
            float angle = 0;
            angle = count == 0 ? angle1 : angle2;
            count = count == 0 ? 1 : 0;
            /*Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            left_eye.transform.rotation = Quaternion.Slerp(left_eye.transform.rotation, rotation, rotate_speed * Time.deltaTime);*/
            yield return rotateOverTime(angle, 2);
            print(count);
        }
    }

    private IEnumerator rotateOverTime(float angle, float duration)
    {
        float counter = 0f;
        while (counter < duration)
        {
            if (Time.timeScale == 0)
                counter += Time.unscaledDeltaTime;
            else
                counter += Time.deltaTime;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            left_eye.transform.rotation = Quaternion.Slerp(left_eye.transform.rotation, rotation, counter/duration);
            right_eye.transform.rotation = Quaternion.Slerp(right_eye.transform.rotation, rotation, counter / duration);

            yield return null;
        }
    }
}
