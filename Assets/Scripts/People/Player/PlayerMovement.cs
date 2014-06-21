using UnityEngine;
using System.Collections;

public class PlayerMovement : PeopleMovementScript
{
	public float jumpPower = 1000;
	private bool canJump;

	override public void handleJump ()
	{
		if (Input.GetButtonUp ("Jump") && canJump) {
			gameObject.rigidbody2D.AddForce (new Vector2 (0f, jumpPower));
			canJump = false;
		}
	}

	override public void handleMovement ()
	{
		if (Input.GetAxis ("Horizontal") != 0) {
			mAcceleration.x += Input.GetAxis ("Horizontal") * accelerationFactor * Time.deltaTime;

			if ((mAcceleration.x < 0 && mVelocity.x > 0) ||
				(mAcceleration.x > 0 && mVelocity.x < 0)) {
				mVelocity.x = 0;
			}
		} else {
			mVelocity /= 1.2f;
		}
		
		Vector3 displacement = mVelocity * Time.deltaTime;
		transform.position += displacement;
		mVelocity += mAcceleration * Time.deltaTime;
		
		if (mVelocity.x > MAX_SPEED) {
			mVelocity.x = MAX_SPEED;
		} else if (mVelocity.x < -MAX_SPEED) {
			mVelocity.x = -MAX_SPEED;
		}
		
		mAcceleration.x = 0;
	}

	protected void OnTriggerEnter2D (Collider2D other)
	{
		canJump = true;
	}
}
