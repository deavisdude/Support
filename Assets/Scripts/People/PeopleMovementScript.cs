using UnityEngine;
using System.Collections;

public abstract class PeopleMovementScript : MonoBehaviour
{
	// ==================================================
	// Constants
	// ==================================================
	
	public const float ACCELERATION_FACTOR = 1000;
	public const float MAX_SPEED = 10f;
	
	// ==================================================
	// Variables
	// ==================================================
	
	protected Vector3 mAcceleration = new Vector3 (0, 0, 0);
	protected Vector3 mVelocity = new Vector3 (0, 0, 0);
	
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
		handleJump ();
		handleMovement ();
	}
}
