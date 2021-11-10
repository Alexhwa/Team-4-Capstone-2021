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
	private bool jumpLastFrame;
	
	//Ledgegrab Check
	[SerializeField] private GameObject ledgeGrab;
	[SerializeField] private float edgeJumpForce = 11.25f;
	private int ledgeLayer = 2;
	private float gravity = 2.3f;

	//Ledgegrab Check
	[SerializeField] private GameObject climbGrab;
	private int climbLayer = 512;

	//Ground check
	[SerializeField] private float minDistFromGround = 1;
	[SerializeField] private GameObject groundCheck;
	[SerializeField] private LayerMask groundMask;
	public bool grounded;
	private Vector2 groundAngle;

	//Audio
	[SerializeField] private AudioClip footstepAClip;
	
	//Animation
	public Animator anim;
	public SpriteRenderer sprtRnd;
	
	//State
	private enum PlayerState
	{
		Idle, Crouch, Hang, Run, Fall, Climb
	}
	private PlayerState currentState;

	private PlayerDeath deathScript;
	
	// Start is called before the first frame update
    void Start()
    {
	    currentState = PlayerState.Idle;
	    rb = GetComponent<Rigidbody2D>();
	    deathScript = GetComponent<PlayerDeath>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
	    if (other.tag.Equals("KillZone"))
	    {
		    deathScript.damagePlayer(1);
	    }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
	    if (Keyboard.current.escapeKey.isPressed)
	    {
		    Application.Quit();
	    }
		grounded = GroundCheck();
		if (deathScript.playerHealth > 0)
		{
			AnimatorStateInfo animStateInfo = anim.GetCurrentAnimatorStateInfo(0);
			switch (currentState)
			{
				case PlayerState.Run:
					if (!animStateInfo.IsName("PlayerRun"))
					{
						anim.Play("PlayerRun");
					}

					DoMovement();
					break;
				case PlayerState.Idle:
					if (!animStateInfo.IsName("PlayerIdlev2"))
					{
						anim.Play("PlayerIdlev2");
					}

					DoMovement();
					break;
				case PlayerState.Crouch:
					break;
				case PlayerState.Hang:
					if (!animStateInfo.IsName("PlayerLedgeGrab"))
					{
						anim.Play("PlayerLedgeGrab");
					}

					HangMovement();
					break;
				case PlayerState.Fall:
					if (!animStateInfo.IsName("PlayerJump") && !animStateInfo.IsName("PlayerJumpFall"))
					{
						anim.Play("PlayerJump");
					}

					DoMovement();

					anim.SetFloat("vSpeed", rb.velocity.y);
					break;
				case PlayerState.Climb:
					ClimbMovement();
					if (!animStateInfo.IsName("PlayerClimb"))
					{
						anim.Play("PlayerClimb");
					}
					break;
			}
		}

		jumpLastFrame = InputController.Inst.inputMaster.Player.Move.ReadValue<Vector2>().y > 0;
    }

    private void HangMovement()
    {
		var moveDir = InputController.Inst.inputMaster.Player.Move.ReadValue<Vector2>();
	    if (moveDir.y > 0 && !jumpLastFrame)
	    {
		    var newVel = rb.velocity;
		    newVel.y = jumpForce * 1.4f;
		    rb.velocity = newVel;
		    currentState = PlayerState.Idle;
		    rb.gravityScale = gravity;
	    }

	    
    }

	public void ClimbMovement()
	{
		var moveDir = InputController.Inst.inputMaster.Player.Move.ReadValue<Vector2>();

		 
		rb.velocity = new Vector2(0, moveDir.y * jumpForce);
		if (Mathf.Abs(moveDir.x) > 0)
        {
			if (Mathf.Abs(rb.velocity.x) < maxWalkSpeed)
			{
				climbGrab.SetActive(false);
				StartCoroutine(ClimbCooldown());
				Vector2 walkVector = new Vector2(walkSpeed * Time.deltaTime * moveDir.x, 0);
				walkVector = AccountForSlope(walkVector);
				rb.velocity += walkVector;
				currentState = PlayerState.Fall;
			}
        }
	}

	IEnumerator ClimbCooldown()
    {
		yield return new WaitForSeconds(0.2f);
		climbGrab.SetActive(true);
	}

	public void DoMovement()
    {
	    var moveDir = InputController.Inst.inputMaster.Player.Move.ReadValue<Vector2>();
	    Vector2 walkVector = new Vector2(walkSpeed * Time.deltaTime * moveDir.x, 0);

	    if (!grounded)
	    {
		    var newVel = rb.velocity;
		    newVel.x /= 1.01f;
		    rb.velocity = newVel;
	    }
	    //Side to side
	    if (Mathf.Abs(rb.velocity.x) < maxWalkSpeed)
	    {
		    
		    walkVector = AccountForSlope(walkVector);
		    rb.velocity += walkVector;

		    if (walkVector.x != 0)
		    {
			    TryFipSprite(walkVector.x > 0);
		    }

		    if(Mathf.Abs(walkVector.x) > 0)
		    {
			    currentState = PlayerState.Run;
			    if (grounded)
			    {
				    AudioManager.Instance.PlaySfx(footstepAClip);
			    }
		    }
		    else
		    {
			    currentState = PlayerState.Idle;
		    }
	    }
	    //	ledge hang check
	    bool hanging = false;
	    if(CanHangFromLedge())
	    {
		    print("we're on a ledge");
		    currentState = PlayerState.Hang;
		    rb.velocity *= 0;
		    rb.gravityScale = 0;
		    return;
	    }
	    else
		    rb.gravityScale = gravity;

        if (CanClimb())
        {
			print("we're on a rope");
			currentState = PlayerState.Climb;
			rb.velocity *= 0;
			rb.gravityScale = 0;
			return;
		}

	    //jump
	    if (moveDir.y > 0 && !jumpLastFrame && (grounded || hanging))
	    {
		    var newVel = rb.velocity;
		    newVel.y = jumpForce;
		    rb.velocity = newVel;
	    }

	    if (!grounded)
	    {
		    currentState = PlayerState.Fall;
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

	private bool CanClimb()
	{
		return (Physics2D.IsTouchingLayers(climbGrab.GetComponent<Collider2D>(), climbLayer));
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

    private void TryFipSprite(bool isMovingRight)
    {
	    if (isMovingRight && sprtRnd.flipX)
	    {
		    sprtRnd.flipX = false;
	    }
	    else if(!isMovingRight && !sprtRnd.flipX)
	    {
		    sprtRnd.flipX = true;
	    }
    }
}
