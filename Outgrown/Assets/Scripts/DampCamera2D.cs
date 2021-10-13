// Smooth towards the target

using System;
using UnityEngine;
using System.Collections;
     
public class DampCamera2D : MonoBehaviour
{
    public Transform target;
    public float smoothTime = 0.3f;
    private Vector3 velocity = Vector3.zero;
    [SerializeField] private float delayFollow;
    public bool following;
    public Vector3 offset;

    private void Start()
    {
        if (delayFollow > 0 && !Preloaded.Instance.sceneChange)
        {
            StartCoroutine(StartFollowing(delayFollow));
        }
        else
        {
            following = true;
        }
    }

    private IEnumerator StartFollowing(float delay)
    {
        yield return new WaitForSeconds(delay);
        following = true;
    }
    void Update()
    {
        if (following)
        {
            // Define a target position above and behind the target transform
            Vector3 targetPosition = target.TransformPoint(new Vector3(0, 0, -10)) + offset;

            // Smoothly move the camera towards that target position
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothTime);
        }
    }
}