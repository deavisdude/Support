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
			mFollowers.Add (followerObject);
		}
	}

	// ====================
	// Lifecycle Methods
	// ====================

	void Update ()
	{
	
	}
}
