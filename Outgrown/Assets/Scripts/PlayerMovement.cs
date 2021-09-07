using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	[SerializeField] private float accelFactor = 1;
	[SerializeField] private float gravity = 9.8f;
	[SerializeField] private Vector2 veloc;
	[SerializeField] private float jumpForce = 1;
	[SerializeField] private float minDistFromGround = 1;
	
	[SerializeField] private GameObject groundCheck;
	private bool grounded;

	private enum PlayerState
	{
		Idle, Crouch, Hang, Run, Fall
	}
	private PlayerState currentState;
	
	
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
	    var gamePad = Gamepad.current;
	    grounded = GroundCheck();
	    switch (currentState)
	    {
		    case PlayerState.Idle:
			    
			    MoveCheck(0);
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
		transform.position += new Vector3(veloc.x, veloc.y, 0);
		
    }

    private void MoveCheck(float horzInput)
    {
	    //  analyze left/right input
	    
	    //	apply lerp: accel/decel
	    veloc.x = Mathf.Lerp(veloc.x, horzInput, Time.deltaTime);
    }
    private bool GroundCheck()
    {
	    RaycastHit2D hit = Physics2D.Raycast(groundCheck.transform.position, Vector2.down);
	    if (hit.collider != null)
	    {
		    float groundDistance = Mathf.Abs(hit.point.y - groundCheck.transform.position.y);
	    }

	    return false;
    }

    private void InputChecks()
    {
	    float horzInput = 0;
	    horzInput += Convert.ToInt32(Input.GetKey("right"));
	    horzInput -= Convert.ToInt32(Input.GetKey("left"));
    }
}
