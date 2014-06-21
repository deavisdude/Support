using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	// ==================================================
	// Constants
	// ==================================================

	public float jumpPower = 1000;
	public float accelerationFactor = 1000;
	public const float MAX_SPEED = 7f;

	// ==================================================
	// Variables
	// ==================================================

	private bool canJump;
	public Vector3 mAcceleration = new Vector3 (0, 0, 0);
	private Vector3 mVelocity = new Vector3 (0, 0, 0);

	// ==================================================
	// Methods
	// ==================================================


	private void handleJump ()
	{
		if(Input.GetButtonUp("Jump") && canJump){
			gameObject.rigidbody2D.AddForce(new Vector2(0f, jumpPower));
			canJump = false;
		}
	}

	private void adjustAcceleration ()
	{
		if (Input.GetAxis ("Horizontal") != 0) {
			mAcceleration.x += Input.GetAxis ("Horizontal") * accelerationFactor * Time.deltaTime;
			if((mAcceleration.x < 0 && mVelocity.x > 0) ||
			   (mAcceleration.x > 0 && mVelocity.x < 0)){
				mVelocity.x=0;
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

	// =========================
	// Lifecycle Methods
	// =========================
	
	protected void Update ()
	{
		handleJump ();
		adjustAcceleration ();
		//Debug.Log(canJump);
	}

	protected void OnTriggerEnter2D(Collider2D other){
		canJump=true;
		Debug.Log(canJump);
	}
}
