using UnityEngine;
using System.Collections;
using SPSUGameJam;

public abstract class PeopleMovementScript : SPSUGameJamScript
{
	// ==================================================
	// Constants
	// ==================================================
	
	public const float ACCELERATION_FACTOR = 1000;
	protected Vector2 MAX_SPEED = new Vector2 (5, 5);
	protected float MAX_JUMP = 7;

	public const float JUMP_POWER = .5f;

	// ==================================================
	// Variables
	// ==================================================

	public Animator anim;

	public Direction currentDirection;
	
	public Vector2 mAcceleration = new Vector2 (0, 0);
	public Vector2 mVelocity = new Vector2 (0, 0);

	protected bool canJump;
	protected bool jumping = false;
	protected float jumpMultiplier;
	
	// ==================================================
	// Methods
	// ==================================================

	public void jump ()
	{

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

	private void determineAnimation ()
	{
		if (rigidbody2D.velocity.x != 0) {
			anim.SetBool ("walking", true);
		} else {
			anim.SetBool ("walking", false);
		}
	}
	// =========================
	// Lifecycle Methods
	// =========================
	
	protected void FixedUpdate ()
	{
		handleMovement ();
		handleJump ();

		if (jumping && Input.GetButtonUp ("Jump")) {
			jumping = false;
		}
		
		if (rigidbody2D.velocity.y > MAX_JUMP) {
			jumping = false;
		}
		
		if (!jumping && rigidbody2D.velocity.y < 0) {
			rigidbody2D.velocity -= new Vector2 (0f, JUMP_POWER);
		}

		assignDirection ();
		determineAnimation ();
	}

	// =========================
	// Abstract Methods
	// ======================
	
	public abstract void handleJump ();
	
	public abstract void handleMovement ();
}
