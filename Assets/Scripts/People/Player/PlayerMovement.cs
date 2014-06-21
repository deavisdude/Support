using UnityEngine;
using System.Collections;
using SPSUGameJam;

public class PlayerMovement : PeopleMovementScript
{
	private bool canJump;
	public Animator anim;

	override public void handleJump ()
	{
		if (Input.GetButton ("Jump") && canJump) {
			gameObject.rigidbody2D.velocity += new Vector2 (0f, JUMP_POWER);
			//mVelocity = rigidbody2D.velocity;
			canJump = false;
		}
	}

	override public void handleMovement ()
	{
		if (Input.GetAxis ("Horizontal") != 0) {
			anim.SetBool("walking", true);
			mAcceleration.x += Input.GetAxis ("Horizontal") * ACCELERATION_FACTOR * Time.deltaTime;


			if (!canJump) {
				mAcceleration.x = 0;
			} else if ((mAcceleration.x < 0 && mVelocity.x > 0) ||
				(mAcceleration.x > 0 && mVelocity.x < 0)) {
				mVelocity.x = 0;
			}
		} else {
			mVelocity /= 1.2f;
			anim.SetBool("walking", false);
		}

		rigidbody2D.velocity += mAcceleration * Time.deltaTime;
		
		if (rigidbody2D.velocity.x > MAX_SPEED.x) {
			rigidbody2D.velocity = new Vector2 (MAX_SPEED.x, rigidbody2D.velocity.y);
		} else if (rigidbody2D.velocity.x < -MAX_SPEED.x) {
			rigidbody2D.velocity = new Vector2 (-MAX_SPEED.x, rigidbody2D.velocity.y);
		}
		
		mAcceleration.x = 0;
	}

	protected void OnCollisionEnter2D (Collision2D other)
	{
		canJump = true;
	}
}
