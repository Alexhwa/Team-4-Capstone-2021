using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	//Movement
	[SerializeField] private float walkSpeed = 10;
	[SerializeField] private float maxWalkSpeed = 15;
	[SerializeField] private float jumpForce = 1;
	private Rigidbody2D rb;
	private Vector2 walkVectorDebug;
	
	//Ground check
	[SerializeField] private float minDistFromGround = 1;
	[SerializeField] private GameObject groundCheck;
	[SerializeField] private LayerMask groundMask;
	public bool grounded;
	private Vector2 groundAngle;

	//State
	private enum PlayerState
	{
		Idle, Crouch, Hang, Run, Fall
	}
	private PlayerState currentState;
	
	// Start is called before the first frame update
    void Start()
    {
	    currentState = PlayerState.Idle;
	    rb = GetComponent<Rigidbody2D>();
	    
    }
    
    // Update is called once per frame
    void Update()
    {
	    grounded = GroundCheck();
	    switch (currentState)
	    {
		    case PlayerState.Idle:
			    DoMovement();
			    break;
		    case PlayerState.Crouch:
			    break;
	    }
        
		
		//	jump
		// if(/* we're on the ground or on an edge */) {
		// 	if(Input.GetKey("jump")) {
		// 		veloc.y = jumpForce;
		// 	} else if(veloc.y != 0) {
		// 		veloc.y = 0;
		// 	}
		// }
		// //	fall
		// else {
		// 	veloc.y -= gravity * Time.deltaTime;
		// }
		
		//	hit wall or ceiling
		// if(/* collide with wall */) {
		// 	veloc.x = 0;
		// }
		// if(/* collide with ceiling */ && veloc.y != 0) {
		// 	veloc.y = 0;
		// }
		
        //  update position
		// transform.position += new Vector3(veloc.x, veloc.y, 0);
		
    }
	
    public void DoMovement()
    {
	    var moveDir = InputController.Inst.inputMaster.Player.Move.ReadValue<Vector2>();
	    //Side to side
	    if (Mathf.Abs(rb.velocity.x) < maxWalkSpeed)
	    {
		    Vector2 walkVector = new Vector2(walkSpeed * Time.deltaTime * moveDir.x, 0);
		    float rotateAngle = Vector2.Angle(groundAngle, Vector2.up);
		    print(groundAngle);
		    print(rotateAngle);
		    if (walkVector.y > 0)
		    {
			    walkVector *= 4f;
		    }
		    walkVectorDebug = Macros.Rotate(walkVector, -rotateAngle);
		    rb.velocity += Macros.Rotate(walkVector, rotateAngle);
	    }

	    if (moveDir.y > 0 && grounded)
	    {
		    var newVel = rb.velocity;
		    newVel.y = jumpForce;
		    rb.velocity = newVel;
	    }
    }
    private bool GroundCheck()
    {
	    RaycastHit2D hit = Physics2D.Raycast(groundCheck.transform.position, Vector2.down, minDistFromGround, groundMask);
	    if (hit.collider != null)
	    {
		    groundAngle = hit.normal;
		    return true;
	    }
	    return false;
    }

    private void OnDrawGizmos()
    { 
	    Gizmos.DrawRay(new Ray(groundCheck.transform.position, walkVectorDebug));
    }
}
