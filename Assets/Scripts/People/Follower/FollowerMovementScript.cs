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
			gameObject.layer = LayerMask.NameToLayer ("followingPeople");
			gameObject.rigidbody2D.isKinematic = false;
			gameObject.collider2D.isTrigger = false;
			mFollowerManagerScript.addFollower (gameObject);
		}
	}
}
