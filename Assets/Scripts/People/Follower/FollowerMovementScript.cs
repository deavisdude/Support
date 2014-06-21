using UnityEngine;
using System.Collections;

public class FollowerMovementScript : PeopleMovementScript
{
	// ==================================================
	// Constants
	// ==================================================
	
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
			mAcceleration.x += ACCELERATION_FACTOR * Time.deltaTime;
		} else if (mTargetPosition.x < currentPosition.x) {
			mAcceleration.x -= ACCELERATION_FACTOR * Time.deltaTime;
		}

		rigidbody2D.AddForce (mVelocity);
		mVelocity += mAcceleration * Time.deltaTime;

		if (mVelocity.x > MAX_SPEED) {
			mVelocity.x = MAX_SPEED;
		} else if (mVelocity.x < -MAX_SPEED) {
			mVelocity.x = -MAX_SPEED;
		}
		
		mAcceleration.x = 0;
	}

	// =========================
	// Lifecycle Methods
	// =========================

	protected void Start ()
	{
		mFollowerManagerScript = mFollowerManager.GetComponent<FollowerManager> ();
	}

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
