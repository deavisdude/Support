using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	// ==================================================
	// Constants
	// ==================================================

	public const float accelerationFactor = 1000;
	public const float MAX_SPEED = 10f;

	// ==================================================
	// Variables
	// ==================================================
	
	public Vector3 mAcceleration = new Vector3 (0, 0, 0);
	private Vector3 mVelocity = new Vector3 (0, 0, 0);

	// ==================================================
	// Methods
	// ==================================================


	private void handleJump ()
	{

	}

	private void adjustAcceleration ()
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

	// =========================
	// Lifecycle Methods
	// =========================
	
	protected void Update ()
	{
		handleJump ();
		adjustAcceleration ();
	}
}
