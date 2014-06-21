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
			rigidbody2D.velocity += new Vector2 (0f, JUMP_POWER);
			canJump = false;
		}
	}

	override public void handleMovement ()
	{
		if (Input.GetAxisRaw ("Horizontal") != 0) {
			anim.SetBool ("walking", true);

			mAcceleration.x = Input.GetAxis ("Horizontal") * ACCELERATION_FACTOR * Time.deltaTime;

			if (!canJump) {
				mAcceleration.x = 0;
			} else if ((mAcceleration.x < 0 && mVelocity.x > 0) ||
				(mAcceleration.x > 0 && mVelocity.x < 0)) {
				mVelocity.x = 0;
			}

			rigidbody2D.velocity += mAcceleration * Time.deltaTime;
		} else {
			if (canJump)
				rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x * 0.8f, rigidbody2D.velocity.y);
			anim.SetBool ("walking", false);
		}
		
		if (rigidbody2D.velocity.x > MAX_SPEED.x) {
			rigidbody2D.velocity = new Vector2 (MAX_SPEED.x, rigidbody2D.velocity.y);
		} else if (rigidbody2D.velocity.x < -MAX_SPEED.x) {
			rigidbody2D.velocity = new Vector2 (-MAX_SPEED.x, rigidbody2D.velocity.y);
		}
	}

	protected void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer ("platform")) {
			canJump = true;
		}
	}
}
