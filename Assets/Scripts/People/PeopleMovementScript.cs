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

	public const float JUMP_POWER = 5f;

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
		if (mVelocity.x > 0) {
			currentDirection = Direction.RIGHT;
		} else if (mVelocity.x < 0) {
			currentDirection = Direction.LEFT;
		}
	}

	// =========================
	// Lifecycle Methods
	// =========================
	
	protected void Update ()
	{
		handleMovement ();
		handleJump ();
		assignDirection ();
	}
}
