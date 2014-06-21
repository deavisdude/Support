using UnityEngine;
using System.Collections;

public class PlayerMovement : PeopleMovementScript
{
	private bool canJump;

	override public void handleJump ()
	{
		if (Input.GetButton ("Jump") && canJump) {
			gameObject.rigidbody2D.AddForce (new Vector2 (0f, JUMP_POWER));
			canJump = false;
		}
	}

	override public void handleMovement ()
	{
		if (Input.GetAxis ("Horizontal") != 0) {
			mAcceleration.x += Input.GetAxis ("Horizontal") * ACCELERATION_FACTOR * Time.deltaTime;


			if (!canJump) {
				mAcceleration.x = 0;
			} else if ((mAcceleration.x < 0 && mVelocity.x > 0) ||
				(mAcceleration.x > 0 && mVelocity.x < 0)) {
				mVelocity.x = 0;
			}
		} else {
			mVelocity /= 1.2f;
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

	protected void OnCollisionEnter2D (Collision2D other)
	{
		canJump = true;
	}
}
