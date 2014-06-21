using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	// ==================================================
	// Constants
	// ==================================================

	private Vector3 mHorizontalAccelerationChange = new Vector3 (30, 0, 0);

	// ==================================================
	// Variables
	// ==================================================

	public Vector3 mAcceleration = new Vector3 (0, 0, 0);
	private Vector3 mVelocity = new Vector3 (0, 0, 0);

	// ==================================================
	// Methods
	// ==================================================


	private void jump ()
	{

	}

	private void adjustAcceleration ()
	{
		if (Input.GetKey (KeyCode.RightArrow)) {
			mAcceleration += mHorizontalAccelerationChange * Time.deltaTime;
		} else if (Input.GetKey (KeyCode.LeftArrow)) {
			mAcceleration -= mHorizontalAccelerationChange * Time.deltaTime;
		} else {
			mAcceleration *= 0;
			mVelocity /= 2f * Time.deltaTime;
		}

		Vector3 displacement = mVelocity * Time.deltaTime;
		transform.position += displacement;
		mVelocity += mAcceleration * Time.deltaTime;
	}

	// =========================
	// Lifecycle Methods
	// =========================
	
	protected void Update ()
	{
		adjustAcceleration ();
	}
}
