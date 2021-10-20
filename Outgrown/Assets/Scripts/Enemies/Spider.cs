using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class Spider : MonoBehaviour
{
    public Transform bodyTransform;
    public Vector2 raycastDirection;
    public float minDistAway;
    [Range(0f,1f)]
    public float bodyYAdjustSpeed;
    
    public LayerMask groundMask;
    
    private enum SpiderState {Inactive, Begin, Chase};
    SpiderState currentState = SpiderState.Inactive;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastToGround();
    }

    private void RaycastToGround()
    {
        
        RaycastHit2D hit = Physics2D.Raycast(bodyTransform.position,raycastDirection, 10000, groundMask);
        if (hit.collider != null)
        {
            if (Vector3.Distance(hit.point, bodyTransform.position) < minDistAway)
            {
                print("raycast threshold reached");
                Vector3 newYLoc = hit.point;
                newYLoc.x = bodyTransform.position.x;
                newYLoc.y += minDistAway;
                bodyTransform.position = Vector3.Lerp(bodyTransform.position, newYLoc, bodyYAdjustSpeed);
            }
            else
            {
                Vector3 newYLoc = hit.point;
                newYLoc.x = bodyTransform.position.x;
                newYLoc.y += minDistAway;
                bodyTransform.position = Vector3.Lerp(bodyTransform.position, newYLoc, bodyYAdjustSpeed);
            }
        }
        else
        {
            Vector3 newYLoc = bodyTransform.position;
            newYLoc.y = -100;
            bodyTransform.position = Vector3.Lerp(bodyTransform.position, newYLoc, .07f);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(bodyTransform.position, raycastDirection);
    }
}
