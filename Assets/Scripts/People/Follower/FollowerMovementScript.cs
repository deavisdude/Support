﻿using UnityEngine;
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
	public bool shouldJumpBool = false;

	public GameObject mFollowerManager;
	public FollowerManager mFollowerManagerScript;
	public Vector3 mTargetPosition;

	// ==================================================
	// Methods
	// ==================================================

	override public bool shouldJump ()
	{
		return (isOnFloor && shouldJumpBool);
	}

	override public void handleMovement ()
	{
		if (shouldFollow) {
			float velocityX = 0;

			if (!isOnTarget ()) {
				if (isFarFromTargetPosition ()) {
					velocityX = MAX_SPEED;
				} else if (isNearTargetPosition ()) {
					velocityX = MAX_SPEED / 1.5f;
				}

				if (!(mTargetPosition.x > transform.position.x)) {
					velocityX *= -1;
				}
			}

			Vector2 newVelocity = new Vector2 (velocityX, rigidbody2D.velocity.y);
			rigidbody2D.velocity = newVelocity;
		}
	}

	override public void onJump ()
	{
		audioManager.playJumpSound ();
	}

	protected void onLandOnFloor ()
	{
		base.onLandOnFloor ();
		shouldJumpBool = false;
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

	private bool isOnTarget ()
	{
		if (transform.position.x > mTargetPosition.x - TARGET_POSITION_VALID_OFFSET 
			&& transform.position.x < mTargetPosition.x + TARGET_POSITION_VALID_OFFSET) {
			return true;
		}
		
		return false;
	}

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
