using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement_LedgeTest : MonoBehaviour
{
	//Movement
	[SerializeField] private float walkSpeed = 10;
	[SerializeField] private float maxWalkSpeed = 15;
	[SerializeField] private float jumpForce = 1;
	private Rigidbody2D rb;
	private Vector2 walkVectorDebug;
	
	//Ledgegrab Check
	[SerializeField] private GameObject ledgeGrab;
	private int ledgeLayer = 2;

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
        
		if(canHangFromLedge())
			print("we're on a ledge");
    }
	
    public void DoMovement()
    {
	    var moveDir = InputController.Inst.inputMaster.Player.Move.ReadValue<Vector2>();
	    //Side to side
	    if (Mathf.Abs(rb.velocity.x) < maxWalkSpeed)
	    {
		    Vector2 walkVector = new Vector2(walkSpeed * Time.deltaTime * moveDir.x, 0);
		    if (grounded)
		    {
			    float rotateAngle = Vector2.Angle(groundAngle, Vector2.up) + 20;
			    walkVector = Macros.Rotate(walkVector, -rotateAngle);
		    }

		    if (walkVector.y > 0)
		    {
			    walkVector *= 1.05f;
		    }

		    walkVectorDebug = walkVector;
		    rb.velocity += walkVector;
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
	
	//	check if our ledgeGrab collider found a ledge
    private bool canHangFromLedge()
    {
		return (Physics2D.IsTouchingLayers(ledgeGrab.GetComponent<Collider2D>(), ledgeLayer));
	}

    private void OnDrawGizmos()
    { 
	    Gizmos.DrawRay(new Ray(groundCheck.transform.position, walkVectorDebug));
    }
}
