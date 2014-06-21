using UnityEngine;
using System.Collections;

public class FollowerManager : MonoBehaviour
{
	// ==================================================
	// Variables
	// ==================================================

	public GameObject mPlayer;

	ArrayList mFollowers = new ArrayList ();

	// ==================================================
	// Methods
	// ==================================================

	public void addFollower (GameObject followerObject)
	{
		if (!mFollowers.Contains (followerObject)) {
			Debug.Log ("Follower has been added!");
			mFollowers.Add (followerObject);
		}
	}

	// ====================
	// Lifecycle Methods
	// ====================

	void Update ()
	{
		for (int i = 0; i < mFollowers.Count; i ++) {
			GameObject follower = (GameObject)mFollowers [i];
			FollowerMovementScript movementScript = follower.GetComponent<FollowerMovementScript> ();
			movementScript.mTargetPosition = mPlayer.transform.position;
		}
	}
}
