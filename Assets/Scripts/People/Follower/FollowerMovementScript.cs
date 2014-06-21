using UnityEngine;
using System.Collections;

public class FollowerMovementScript : PeopleMovementScript
{
	// ==================================================
	// Constants
	// ==================================================

	private const float TARGET_POSITION_NEAR_MARKER = 2f;
	private const float TARGET_POSITION_VALID_OFFSET = .25f;

	// ==================================================
	// Variables
	// ==================================================

	public bool shouldFollow = false;
	public GameObject mFollowerManager;
	public FollowerManager mFollowerManagerScript;
	public Vector3 mTargetPosition;

	// ==================================================
	// Methods
	// ==================================================
	
	override public void handleJump ()
	{

	}

	override public void handleMovement ()
	{
		if (shouldFollow) {
			float velocityX = 0;

			if (!onTarget ()) {
				if (isFarFromTargetPosition ()) {
					velocityX = MAX_SPEED.x;
				} else if (isNearTargetPosition ()) {
					velocityX = MAX_SPEED.x / 2f;
				}

				if (!(mTargetPosition.x > transform.position.x)) {
					velocityX *= -1;
				}
			}

			Vector2 newVelocity = new Vector2 (velocityX, rigidbody2D.velocity.y);
			rigidbody2D.velocity = newVelocity;
		}
	}

	private bool isFarFromTargetPosition ()
	{
		return !isNearTargetPosition ();
	}

	private bool isNearTargetPosition ()
	{
		if (transform.position.x > mTargetPosition.x - TARGET_POSITION_NEAR_MARKER 
			&& transform.position.x < mTargetPosition.x + TARGET_POSITION_NEAR_MARKER) {
			return true;
		}

		return false;
	}

	private bool onTarget ()
	{
		if (transform.position.x > mTargetPosition.x - TARGET_POSITION_VALID_OFFSET 
			&& transform.position.x < mTargetPosition.x + TARGET_POSITION_VALID_OFFSET) {
			return true;
		}
		
		return false;
	}

	// =========================
	// Lifecycle Methods
	// =========================

	// =========================
	// Triggered Methods
	// =========================

	protected void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.tag.Equals ("Player")) {
			mFollowerManagerScript.addFollower (gameObject);
			gameObject.layer = LayerMask.NameToLayer ("followingPeople");
			gameObject.rigidbody2D.isKinematic = false;
			gameObject.collider2D.isTrigger = false;
		}
	}
}
