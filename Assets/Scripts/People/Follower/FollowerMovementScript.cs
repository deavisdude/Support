using UnityEngine;
using System.Collections;

public class FollowerMovementScript : PeopleMovementScript
{
	// ==================================================
	// Constants
	// ==================================================

	private const float TARGET_POSITION_LEEWAY = .5f;

	// ==================================================
	// Variables
	// ==================================================

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
		Vector3 currentPosition = transform.position;

		if (mTargetPosition.x > currentPosition.x) {
			mAcceleration.x += ACCELERATION_FACTOR * 2 * Time.deltaTime;
		} else if (mTargetPosition.x < currentPosition.x) {
			mAcceleration.x -= ACCELERATION_FACTOR * 2 * Time.deltaTime;
		}

		rigidbody2D.AddForce (mVelocity);
		mVelocity += mAcceleration * Time.deltaTime;

		if (mVelocity.x > MAX_SPEED.x) {
			mVelocity.x = MAX_SPEED.x;
		} else if (mVelocity.x < -MAX_SPEED.x) {
			mVelocity.x = -MAX_SPEED.x;
		}

		if (isNearTargetPosition ()) {
			mVelocity.x = 0;
		}
		
		mAcceleration.x = 0;
	}

	private bool isNearTargetPosition ()
	{
		if (transform.position.x > mTargetPosition.x - TARGET_POSITION_LEEWAY 
			&& transform.position.x < mTargetPosition.x + TARGET_POSITION_LEEWAY) {
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
		if (other.gameObject.tag.Equals ("player")) {
			mFollowerManagerScript.addFollower (gameObject);
			gameObject.layer = LayerMask.NameToLayer ("followingPeople");
			gameObject.rigidbody2D.isKinematic = false;
			gameObject.collider2D.isTrigger = false;
		}
	}
}
