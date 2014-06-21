using UnityEngine;
using System.Collections;

public class PlayerMovement : PeopleMovementScript
{
	override public void handleJump ()
	{

	}

	override public void handleMovement ()
	{
		if (Input.GetAxis ("Horizontal") != 0) {
			mAcceleration.x += Input.GetAxis ("Horizontal") * accelerationFactor * Time.deltaTime;
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
}
