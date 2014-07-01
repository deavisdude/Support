using UnityEngine;
using System.Collections;

public class FollowerMovementScript : PeopleMovementScript
{
		// ==================================================
		// Constants
		// ==================================================

		private const float TARGET_POSITION_NEAR_MARKER = 2f;
		private const float TARGET_POSITION_VALID_OFFSET = .25f;

		// ==================================================
		// Variables
		// ==================================================

		public Sprite friendlyBase;
		public bool isEnemy = false;
		public bool shouldFollow = false;
		public bool shouldJumpBool = false;

		public Sprite boyClothes;
		public Sprite girlClothes;

		public GameObject mFollowerManager;
		public FollowerManager mFollowerManagerScript;
		public Vector3 mTargetPosition;

		public SpriteRenderer[] spriteRenderers;

		// ==================================================
		// Methods
		// ==================================================

		override public bool shouldJump ()
		{
				return false;
		}

		override public void handleMovement ()
		{
				if (shouldFollow) {
						float velocityX = 0;

						if (!isOnTarget ()) {
								if (isFarFromTargetPosition ()) {
										velocityX = MAX_SPEED;
								} else if (isNearTargetPosition ()) {
										velocityX = MAX_SPEED / 1.5f;
								}

								if (!(mTargetPosition.x > transform.position.x)) {
										velocityX *= -1;
								}
						}

						Vector2 newVelocity = new Vector2 (velocityX, rigidbody2D.velocity.y);
						rigidbody2D.velocity = newVelocity;
				}
		}
	
		override public void onJump ()
		{
				hasJumpedRecently = true;
				audioManager.playJumpSound (0.5f);

				if (!IsInvoking ("resetHasJumpedRecently")) {
						Invoke ("resetHasJumpedRecently", .5f);
				}
		}

		private void resetHasJumpedRecently ()
		{
				hasJumpedRecently = false;
		}

		new protected void onLandOnFloor ()
		{
				base.onLandOnFloor ();
				shouldJumpBool = false;
		}

		private bool isFarFromTargetPosition ()
		{
				return !isNearTargetPosition ();
		}

		private bool isNearTargetPosition ()
		{
				if (transform.position.x > mTargetPosition.x - TARGET_POSITION_NEAR_MARKER 
						&& transform.position.x < mTargetPosition.x + TARGET_POSITION_NEAR_MARKER) {
						return true;
				}

				return false;
		}

		private bool isOnTarget ()
		{
				if (transform.position.x > mTargetPosition.x - TARGET_POSITION_VALID_OFFSET 
						&& transform.position.x < mTargetPosition.x + TARGET_POSITION_VALID_OFFSET) {
						return true;
				}
		
				return false;
		}

		public void clothesChange ()
		{
				if (gameObject.name == "Obstacle" && shouldFollow) {
						SpriteRenderer sr = gameObject.GetComponentInChildren<SpriteRenderer> ();
						sr.sprite = friendlyBase;
				}
		}

		// =========================
		// Lifecycle Methods
		// =========================

		new protected void Start ()
		{
				base.Start ();

				if (shouldFollow) {
						mFollowerManagerScript.addFollower (gameObject, false);
				}

				if (!isEnemy || gameObject.name != "Obstacle") {
						float randomBlue = Random.Range (200, 245);
						float randomGreen = Random.Range (200, 245);
						Color currentColor = new Color (1, randomGreen / 255f, randomBlue / 255);
						GetComponentInChildren<SpriteRenderer> ().material.color = currentColor;
				} else {
						GetComponentInChildren<SpriteRenderer> ().material.color = Exit.enemyColors [3];

						SpriteRenderer aaronIsSleepy = GameObject.Find ("Oclothes").GetComponent<SpriteRenderer> ();

						if (Obstacle.enemyIsBoy) {
								aaronIsSleepy.sprite = boyClothes;
						} else {
								aaronIsSleepy.sprite = girlClothes;
						}

						/*if (Obstacle.enemyIsBoy) {
				GetComponentInChildren<SpriteRenderer> ().sprite = boyClothes;
			} else {
				GetComponentInChildren<SpriteRenderer> ().sprite = girlClothes;
			}*/
				}
		}

		public void Update ()
		{
				clothesChange ();
		}

		override public void thisIsAReallyBadMethodPleaseIgnoreForNow ()
		{
				collidingWithSomething = false;
		}

		// =========================
		// Triggered Methods
		// =========================

		protected void OnTriggerEnter2D (Collider2D other)
		{
				if (other.gameObject.tag.Equals ("Player")) {
						mFollowerManagerScript.addFollower (gameObject);
						gameObject.layer = LayerMask.NameToLayer ("followingPeople");
						gameObject.rigidbody2D.isKinematic = false;

						foreach (Collider2D coll in gameObject.GetComponents<Collider2D>()) {
								coll.isTrigger = false;
						}
				}
		}
}
