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

	public float accelerationFactor = 100;
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
		if(Input.GetAxis("Horizontal") != 0){
			mAcceleration.x += Input.GetAxis("Horizontal") * accelerationFactor * Time.deltaTime;
		} else{
			mVelocity /= 1.2f;
			mAcceleration.x=0;
		}
		Vector3 displacement = mVelocity * Time.deltaTime;
		transform.position += displacement;
		mVelocity += mAcceleration * Time.deltaTime;
		if(mVelocity.x > 3.5f){
			mVelocity.x=3.5f;
		}else if(mVelocity.x <-3.5f){
			mVelocity.x=-3.5f;
		}
		mAcceleration.x=0;
	}

	// =========================
	// Lifecycle Methods
	// =========================
	
	protected void Update ()
	{
		adjustAcceleration ();
	}
}
