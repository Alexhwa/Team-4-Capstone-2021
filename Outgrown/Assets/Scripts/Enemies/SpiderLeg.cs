using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderLeg : MonoBehaviour
{
    public Transform targetTransform;
    public Vector3 targetPosition;
    public Vector2 raycastDirection;
    public LayerMask groundMask;
    public float maxDistBeforeStep;
    public float targetHeightOnStep;
    [Range(0f, 1f)]
    public float stepSpeed;
    
    private Vector3 debugGroundPoint;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = targetTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, raycastDirection, 10000, groundMask);
        if (hit.collider != null)
        {
            print(gameObject.name + Mathf.Abs(hit.point.x - targetTransform.position.x));
            if (Mathf.Abs(hit.point.x - targetTransform.position.x) >= maxDistBeforeStep)
            {
                targetPosition = hit.point + new Vector2(0, targetHeightOnStep);
            }

            debugGroundPoint = hit.point;
        }

        targetTransform.position = Vector3.Lerp(targetTransform.position, targetPosition, stepSpeed);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, raycastDirection);
        
        Gizmos.DrawSphere(debugGroundPoint, 1);
    }
}
