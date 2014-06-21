using UnityEngine;
using System.Collections;
using SPSUGameJam;

public abstract class PeopleMovementScript : MonoBehaviour
{
	// ==================================================
	// Constants
	// ==================================================
	
	public const float ACCELERATION_FACTOR = 1000;
	protected Vector2 MAX_SPEED = new Vector2 (5, 5);
	protected float MAX_JUMP = 7;

	public const float JUMP_POWER = .5f;

	public Direction currentDirection;
	
	// ==================================================
	// Variables
	// ==================================================
	
	public Vector2 mAcceleration = new Vector2 (0, 0);
	public Vector2 mVelocity = new Vector2 (0, 0);
	
	// ==================================================
	// Methods
	// ==================================================
	
	public abstract void handleJump ();
	
	public abstract void handleMovement ();

	private void assignDirection ()
	{
		if (rigidbody2D.velocity.x > 0) {
			currentDirection = Direction.RIGHT;
			transform.localScale = new Vector3(-1,1,1);
		} else if (rigidbody2D.velocity.x < 0) {
			currentDirection = Direction.LEFT;
			transform.localScale = new Vector3(1,1,1);
		}
	}

	// =========================
	// Lifecycle Methods
	// =========================
	
	protected void FixedUpdate ()
	{
		handleMovement ();
		handleJump ();
		assignDirection ();
	}
}
