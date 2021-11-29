using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class SoldierLight : MonoBehaviour
{
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

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag == "Player")
        {
            if (!collider.GetComponent<PlayerCover>().InCover())
            {
                //StartCoroutine(OnAlert());
                StartCoroutine(Arrest());
                gameObject.GetComponent<Light2D>().color = Color.red;
                Vector3 direction;
                direction = transform.position - collider.transform.position;
                direction *= -1;
                transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            }
        }
    }

 /*   private void OnTriggerStay2D(Collider2D collision)
    {
        StartCoroutine(Arrest());
        Vector3 direction;
        direction = transform.position - collision.transform.position;
        direction *= -1;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
    }*/

    private void OnTriggerExit2D(Collider2D collision)
    {
        StopAllCoroutines();
        gameObject.GetComponent<Light2D>().color = Color.white;
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
            Quaternion s = Quaternion.Euler(0, 0, rotateAngle);
            transform.rotation = Quaternion.Slerp(transform.rotation, s, Time.deltaTime);
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
            direction = transform.position - trans.position;
            direction *= -1;
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
            yield return null;
        }
    }

    private IEnumerator Arrest()
    {
        yield return new WaitForSeconds(2);

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerDeath>().damagePlayer(1);
    }
}
