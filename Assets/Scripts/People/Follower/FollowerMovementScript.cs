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
		mFollowerManagerScript.testMethod (gameObject);
	}

	// =========================
	// Triggered Methods
	// =========================

	protected void OnTriggerEnter2D (Collider2D other)
	{
		Debug.Log ("onTriggerEnter");

		if (other.gameObject.tag.Equals ("player")) {
			gameObject.layer = LayerMask.NameToLayer ("followingPeople");
			gameObject.rigidbody2D.isKinematic = false;
			gameObject.collider2D.isTrigger = false;
		}
	}
}
