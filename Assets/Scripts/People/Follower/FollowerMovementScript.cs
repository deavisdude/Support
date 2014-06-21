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

	private bool mFollowPlayer = true;
	private GameObject mPlayer;

	// ==================================================
	// Methods
	// ==================================================
	
	override public void handleJump ()
	{

	}

	override public void handleMovement ()
	{
		if (mFollowPlayer) {
			if (isPlayerOnLeft ()) {

			} else if (isPlayerOnRight ()) {

			} else {

			}
		}
	}

	private bool isPlayerOnLeft ()
	{
		Vector3 playerPosition = mPlayer.transform.position;
		
		if (playerPosition.x < transform.position.x) {
			return true;
		}
		
		return false;
	}

	private bool isPlayerOnRight ()
	{
		Vector3 playerPosition = mPlayer.transform.position;

		if (playerPosition.x > transform.position.x) {
			return true;
		}

		return false;
	}

	// =========================
	// Lifecycle Methods
	// =========================

	protected void Start ()
	{
		mPlayer = GameObject.FindGameObjectWithTag ("Player");
	}
}
