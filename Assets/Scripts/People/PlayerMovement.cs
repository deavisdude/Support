using UnityEngine;
using System.Collections;
using SPSUGameJam;

public class PlayerMovement : PeopleMovementScript
{
	// ==================================================
	// Variables
	// ==================================================
	
	public static bool isBoy;

	public static Color currentColor = new Color (120f / 255f, 120f / 255f, 240f / 255f);

	public FollowerManager followManager;

	public Sprite boyClothes;
	public Sprite girlClothes;
	public static SpriteRenderer baseSpriteRender;

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

	protected void Start ()
	{
		base.Start ();
		GameObject.Find ("clothes").GetComponent<SpriteRenderer> ().sprite = (isBoy) ? boyClothes : girlClothes;
		baseSpriteRender = GetComponentInChildren<SpriteRenderer>();
		baseSpriteRender.color = currentColor;
	}

	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.Escape))
			Application.LoadLevel("menu");
	}
}
