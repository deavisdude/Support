using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowerManager : MonoBehaviour
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
		if (!mFollowers.Contains (followerObject)) {
			mFollowers.Add (followerObject);
			Debug.Log ("Follower has been added! There are now " + mFollowers.Count + " followers!");
		}
	}

	private Vector3 getTargetPositionForIndexFollower (int i)
	{

	}

	// ====================
	// Lifecycle Methods
	// ====================

	protected void Update ()
	{
		for (int i = 0; i < mFollowers.Count; i ++) {
			GameObject follower = (GameObject)mFollowers [i];
			FollowerMovementScript movementScript = follower.GetComponent<FollowerMovementScript> ();
			movementScript.mTargetPosition = getTargetPositionForIndex (i);
		}
	}
}
