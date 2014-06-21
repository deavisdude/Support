using UnityEngine;
using System.Collections;

public abstract class PeopleMovementScript : MonoBehaviour
{
	// ==================================================
	// Constants
	// ==================================================
	
	public const float ACCELERATION_FACTOR = 1000;
	public Vector2 MAX_SPEED = new Vector2 (5, 5);

	public const float JUMP_POWER = 5f;
	
	// ==================================================
	// Variables
	// ==================================================
	
	protected Vector2 mAcceleration = new Vector2 (0, 0);
	protected Vector2 mVelocity = new Vector2 (0, 0);
	
	// ==================================================
	// Methods
	// ==================================================
	
	public abstract void handleJump ();
	
	public abstract void handleMovement ();
	
	// =========================
	// Lifecycle Methods
	// =========================
	
	protected void Update ()
	{
		handleMovement ();
		handleJump ();
	}
}
