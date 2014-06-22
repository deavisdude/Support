using UnityEngine;
using System.Collections;
using SPSUGameJam;

public abstract class PeopleMovementScript : SPSUGameJamScript
{
	// ==================================================
	// Constants
	// ==================================================
	
	protected const float ACCELERATION_FACTOR = 1000;
	protected const float DECCELERATION_RATE = 0.95f;
	protected const float JUMP_POWER = .5f;
	protected const float MAX_JUMP = 7;
	protected const float MAX_SPEED = 5f;

	// ==================================================
	// Variables
	// ==================================================

	public Animator walkingAnimation;

	protected bool isOnFloor = true;
	protected bool jumping = false;

	public Direction currentDirection;

	protected float jumpMultiplier;
	
	// ==================================================
	// Methods
	// ==================================================

	public void jump ()
	{
		Debug.Log ("jump");
		if (!jumping) {
			Vector2 addedForce = new Vector2 (0f, 400f);
			rigidbody2D.AddForce (addedForce);
			jumping = true;
		}
	}

	protected void onLandOnFloor ()
	{
		isOnFloor = true;
	}

	private void assignDirection ()
	{
		if (rigidbody2D.velocity.x > 0) {
			currentDirection = Direction.RIGHT;
			transform.localScale = new Vector3 (-1, 1, 1);
		} else if (rigidbody2D.velocity.x < 0) {
			currentDirection = Direction.LEFT;
			transform.localScale = new Vector3 (1, 1, 1);
		}
	}

	private void adjustForMaxSpeed ()
	{
		if (rigidbody2D.velocity.x > MAX_SPEED) {
			rigidbody2D.velocity = new Vector2 (MAX_SPEED, rigidbody2D.velocity.y);
		} else if (rigidbody2D.velocity.x < -MAX_SPEED) {
			rigidbody2D.velocity = new Vector2 (-MAX_SPEED, rigidbody2D.velocity.y);
		}
	}

	private void handleJump ()
	{
		if (shouldJump ()) {
			if (isOnFloor) {
				onJump ();
				jumpMultiplier = 5;
			}
			
			rigidbody2D.velocity += new Vector2 (0f, JUMP_POWER * jumpMultiplier);
			jumpMultiplier *= .75f;
			isOnFloor = false;
			jumping = true;
		}

		if (jumping && Input.GetButtonUp ("Jump")) {
			jumping = false;
		}
		
		if (rigidbody2D.velocity.y > MAX_JUMP) {
			jumping = false;
		}
		
		if (!jumping && rigidbody2D.velocity.y < 0) {
			rigidbody2D.velocity -= new Vector2 (0f, JUMP_POWER);
		}
	}

	private void determineAnimation ()
	{
		if (rigidbody2D.velocity.x != 0) {
			walkingAnimation.SetBool ("walking", true);
		} else {
			walkingAnimation.SetBool ("walking", false);
		}
	}

	// =========================
	// Lifecycle Methods
	// =========================
	
	protected void FixedUpdate ()
	{
		handleMovement ();
		adjustForMaxSpeed ();
		handleJump ();
		assignDirection ();
		determineAnimation ();
	}

	// =========================
	// Triggered Methods
	// =========================

	protected void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer ("platform")) {
			if (!isOnFloor) {
				onLandOnFloor ();
			}
		}
	}

	protected void OnCollisionExit2D (Collision2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer ("platform")) {
			if (isOnFloor) {
				isOnFloor = false;
			}
		}
	}

	// =========================
	// Abstract Methods
	// =========================
	
	public abstract bool shouldJump ();

	public abstract void handleMovement ();

	public abstract void onJump ();
}
