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

	private bool mFollowPlayer = false;
	private GameObject mPlayer;

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
		mPlayer = GameObject.FindGameObjectWithTag ("Player");
	}
}
