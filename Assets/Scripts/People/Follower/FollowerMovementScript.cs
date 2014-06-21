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
			Vector3 playerPosition = mPlayer.transform.position;

			if (playerPosition.x > transform.position.x) {

			} else if (playerPosition.x < transform.position.x) {
			
			}
		}
	}

	// =========================
	// Lifecycle Methods
	// =========================

	protected void Start ()
	{
		mPlayer = GameObject.FindGameObjectWithTag ("Player");
	}
}
