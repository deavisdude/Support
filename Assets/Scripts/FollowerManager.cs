using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using SPSUGameJam;
using Holoville.HOTween;

public class FollowerManager : SPSUGameJamScript
{
		// ==================================================
		// Variables
		// ==================================================

		public GameObject mPlayer;

		public List<GameObject> mFollowers = new List<GameObject> ();

		// ==================================================
		// Methods
		// ==================================================

		public void addFollower (GameObject followerObject)
		{
				int nextLevel = Exit.GetNextLevelColorIndex ();

				if (nextLevel < 4) {
						HOTween.To (PlayerMovement.baseSpriteRender, 1, "color", Exit.playerColors [nextLevel]);
				}

				addFollower (followerObject, true);
		}

		public void addFollower (GameObject followerObject, bool triggerNextMusicLoop)
		{
				if (!mFollowers.Contains (followerObject)) {
						FollowerMovementScript movementScript = followerObject.GetComponent<FollowerMovementScript> ();
						movementScript.shouldFollow = true;
						mFollowers.Add (followerObject);

						if (triggerNextMusicLoop) {
								audioManager.onHitFollower ();
								audioManager.playNewFriendSound ();
						}
				}
		}

		public IEnumerator triggerJumpSequence ()
		{
				for (int i = 0; i < mFollowers.Count; i ++) {
						GameObject follower = (GameObject)mFollowers [i];
						FollowerMovementScript movementScript = follower.GetComponent<FollowerMovementScript> ();
						yield return new WaitForSeconds (.85f);
						movementScript.jump ();
				}
		}

		private Vector3 getTargetPositionForIndexFollower (int i)
		{
				PlayerMovement movementScript = mPlayer.GetComponent<PlayerMovement> ();
				Direction directionOfPlayer = movementScript.currentDirection;
				float xDisplacement = 2.5f * (i + 1) * ((directionOfPlayer == Direction.RIGHT) ? -1f : 1f);
				Vector3 targetPosition = new Vector3 (mPlayer.transform.position.x + xDisplacement, 0, 0);
				return targetPosition;
		}

		// ====================
		// Lifecycle Methods
		// ====================

		protected void Update ()
		{
				for (int i = 0; i < mFollowers.Count; i ++) {
						GameObject follower = (GameObject)mFollowers [i];
						FollowerMovementScript movementScript = follower.GetComponent<FollowerMovementScript> ();
						movementScript.mTargetPosition = getTargetPositionForIndexFollower (i);
				}
		}
}
