using UnityEngine;
using System.Collections;
using SPSUGameJam;

public class PlayerMovement : PeopleMovementScript
{
	// ==================================================
	// Variables
	// ==================================================
	
	public static bool isBoy;

	public FollowerManager followManager;

	public Sprite boyClothes;
	public Sprite girlClothes;

	// ==================================================
	// Methods
	// ==================================================

	override public bool shouldJump ()
	{
		return (Input.GetButton ("Jump") && (isOnFloor || jumping));
	}

	override public void handleMovement ()
	{
		if (Input.GetAxisRaw ("Horizontal") != 0) {
			Vector2 addedForce = new Vector2 (Input.GetAxis ("Horizontal") * ACCELERATION_FACTOR * Time.fixedDeltaTime, 0f);

			if (!isOnFloor) {
				addedForce /= 2;
			}

			rigidbody2D.AddForce (addedForce);
		} else {
			if (isOnFloor) {
				rigidbody2D.velocity = new Vector2 (rigidbody2D.velocity.x * 0.95f, rigidbody2D.velocity.y);
			}
		}
	}

	override public void onJump ()
	{
		StartCoroutine (followManager.triggerJumpSequence ());
		audioManager.playJumpSound ();
	}
	
	// =========================
	// Lifecycle Methods
	// =========================

	void Start ()
	{
		base.Start ();
		
		if (isBoy) {
			GameObject.Find ("clothes").GetComponent<SpriteRenderer> ().sprite = boyClothes;
		} else {
			GameObject.Find ("clothes").GetComponent<SpriteRenderer> ().sprite = girlClothes;
		}
	}
}
