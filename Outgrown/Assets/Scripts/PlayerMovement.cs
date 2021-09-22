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
	
	//Ledgegrab Check
	[SerializeField] private GameObject ledgeGrab;
	[SerializeField] private float edgeJumpForce = 11.25f;
	private int ledgeLayer = 2;
	private float gravity = 2.3f;
	
	//Ground check
	[SerializeField] private float minDistFromGround = 1;
	[SerializeField] private GameObject groundCheck;
	[SerializeField] private LayerMask groundMask;
	public bool grounded;
	private Vector2 groundAngle;

	//Audio
	[SerializeField] private AudioClip footstepAClip;
	
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
		    case PlayerState.Hang:
			    HangMovement();
			    break;
	    }
    }

    private void HangMovement()
    {
	    
    }
    public void DoMovement()
    {
	    var moveDir = InputController.Inst.inputMaster.Player.Move.ReadValue<Vector2>();
	    //Side to side
	    if (Mathf.Abs(rb.velocity.x) < maxWalkSpeed)
	    {
		    Vector2 walkVector = new Vector2(walkSpeed * Time.deltaTime * moveDir.x, 0);
		    walkVector = AccountForSlope(walkVector);
		    rb.velocity += walkVector;
		    
		    if(Mathf.Abs(walkVector.x) > 0)
		    {
			    print("trying to play footsteps");
//			    AudioSource source = AudioManager.Instance.SearchSFX(footstepAClip);
//			    source.volume = .4f;
//			    source.pitch = 1.71f;
//			    AudioManager.Instance.TryPlaySFX(footstepAClip);
		    }
	    }
	    //	ledge hang check
	    bool hanging = false;
	    if(CanHangFromLedge())
	    {
		    print("we're on a ledge");
		    hanging = true;
		    rb.velocity *= 0;
		    rb.gravityScale = 0;
	    }
	    else
		    rb.gravityScale = gravity;

	    //jump
	    if (moveDir.y > 0 && (grounded || hanging))
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
    private bool CanHangFromLedge()
    {
	    return (Physics2D.IsTouchingLayers(ledgeGrab.GetComponent<Collider2D>(), ledgeLayer));
    }

    private void OnDrawGizmos()
    { 
	    Gizmos.DrawRay(new Ray(groundCheck.transform.position, walkVectorDebug));
    }

    private Vector2 AccountForSlope(Vector2 v)
    {
	    Vector2 rotatedV = v;
	    if (grounded)
	    {
		    float rotateAngle = Vector2.Angle(groundAngle, Vector2.up) + 20;
		    //account for non negative result of Vector2.Angle
		    if (groundAngle.x < 0)
		    {
			    rotateAngle *= -1;
		    }
		    rotatedV = Macros.Rotate(v, -rotateAngle);
	    }

	    if (rotatedV.y > 0)
	    {
		    rotatedV *= 1f +  3 * rotatedV.y;
	    }

	    walkVectorDebug = rotatedV;
	    return rotatedV;
    }
}
