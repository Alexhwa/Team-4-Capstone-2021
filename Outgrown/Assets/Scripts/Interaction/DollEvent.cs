using System.Collections;
using System.Collections.Generic;
using Yarn.Unity;
using UnityEngine;

public class DollEvent : MonoBehaviour
{
    [SerializeField] GameObject DollMissingArm;
    [SerializeField] GameObject rope;
    [SerializeField] Vector3 ropeFinalPos = new Vector3(0,0,0);

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
        if (collision.name == "Doll Arm")
        {
            DollMissingArm.SetActive(true);
            Destroy(collision.gameObject);
        }
    }

    public void FixDoll()
    {
        // Doll.GetComponent<SpriteRenderer>().sprite = repairedDoll;
        DollMissingArm.SetActive(true);
        MoveFunction();
    }

    [YarnCommand("ShowRope")]
    public void ShowRope()
    {
        StartCoroutine(MoveFunction());
    }

    IEnumerator MoveFunction()
    {
        print("movefunction");
        float timeSinceStarted = 0f;
        float lerpDuration = 3;
        while (timeSinceStarted < lerpDuration)
        {
            rope.transform.position = Vector3.Lerp(rope.transform.position, ropeFinalPos, timeSinceStarted / lerpDuration);
            timeSinceStarted += Time.deltaTime * .05f;

            // Otherwise, continue next frame
            yield return null;
        }
    }
}
