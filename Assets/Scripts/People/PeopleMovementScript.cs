﻿using UnityEngine;
using System.Collections;
using SPSUGameJam;

public abstract class PeopleMovementScript : SPSUGameJamScript
{
		// ==================================================
		// Constants
		// ==================================================
	
		protected const float ACCELERATION_FACTOR = 1000;
		protected const float DECCELERATION_RATE = 0.95f;
		protected const float JUMP_POWER = .5f;
		protected const float MAX_JUMP = 7.25f;
		protected const float MAX_SPEED = 5f;
		protected bool hasJumpedRecently = false;

		// ==================================================
		// Variables
		// ==================================================

		public Animator walkingAnimation;

		protected bool isOnFloor = true;
		protected bool jumping = false;

		public Direction currentDirection;

		protected float jumpMultiplier;

		protected bool collidingWithSomething = false;
	
		// ==================================================
		// Methods
		// ==================================================

		public void jump ()
		{
				if (!jumping && isOnFloor && !hasJumpedRecently) {
						Vector2 addedForce = new Vector2 (0f, 400f);

						if (!collidingWithSomething) {
								onJump ();
								rigidbody2D.AddForce (addedForce);
								jumping = true;
								collidingWithSomething = true;
						}
				}
		}

		protected void onLandOnFloor ()
		{
				isOnFloor = true;
				collidingWithSomething = false;
		}

		private void assignDirection ()
		{
				if (rigidbody2D.velocity.x > 0) {
						currentDirection = Direction.RIGHT;
						transform.localScale = new Vector3 (-1, 1, 1);
				} else if (rigidbody2D.velocity.x < 0) {
						currentDirection = Direction.LEFT;
						transform.localScale = new Vector3 (1, 1, 1);
				}
		}

		private void adjustForMaxSpeed ()
		{
				if (rigidbody2D.velocity.x > MAX_SPEED) {
						rigidbody2D.velocity = new Vector2 (MAX_SPEED, rigidbody2D.velocity.y);
				} else if (rigidbody2D.velocity.x < -MAX_SPEED) {
						rigidbody2D.velocity = new Vector2 (-MAX_SPEED, rigidbody2D.velocity.y);
				}
		}

		private void handleJump ()
		{
				if (shouldJump ()) {
						if (isOnFloor) {
								onJump ();
								jumpMultiplier = 5;
						}

						rigidbody2D.velocity += new Vector2 (0f, JUMP_POWER * jumpMultiplier);
						jumpMultiplier *= .75f;
						isOnFloor = false;
						jumping = true;
				}
		
				if (jumping && Input.GetButtonUp ("Jump")) {
						jumping = false;
				}
		
				if (rigidbody2D.velocity.y > MAX_JUMP) {
						jumping = false;
				}
		
				if (!jumping && rigidbody2D.velocity.y < 0) {
						rigidbody2D.velocity -= new Vector2 (0f, JUMP_POWER);
				}
		}

		private void determineAnimation ()
		{
				if (Mathf.Abs (rigidbody2D.velocity.x) > 0.5f) {
						walkingAnimation.SetBool ("walking", true);
				} else {
						walkingAnimation.SetBool ("walking", false);
				}
		}

		// =========================
		// Lifecycle Methods
		// =========================
	
		protected void FixedUpdate ()
		{
				handleMovement ();
				adjustForMaxSpeed ();
				handleJump ();
				assignDirection ();
				determineAnimation ();

				if (!IsInvoking ("thisIsBad")) {
						Invoke ("thisIsBad", 1);
				}
		}
	
		// =========================
		// Triggered Methods
		// =========================

		protected void OnCollisionEnter2D (Collision2D other)
		{
				if (other.gameObject.layer == LayerMask.NameToLayer ("platform")) {
						if (!isOnFloor) {
								onLandOnFloor ();
						}
				} else if (other.gameObject.layer != LayerMask.NameToLayer ("plate")) {
						collidingWithSomething = true;
				}

		}

		protected void OnCollisionExit2D (Collision2D other)
		{
				if (other.gameObject.layer == LayerMask.NameToLayer ("platform")) {
						if (isOnFloor) {
								isOnFloor = false;
						}
				} else if (other.gameObject.layer != LayerMask.NameToLayer ("plate")) {
						collidingWithSomething = false;
				}
		}

		// =========================
		// Abstract Methods
		// =========================

		public void thisIsBad ()
		{
				thisIsAReallyBadMethodPleaseIgnoreForNow ();
		}

		public abstract void thisIsAReallyBadMethodPleaseIgnoreForNow ();

		public abstract bool shouldJump ();

		public abstract void handleMovement ();

		public abstract void onJump ();
}
