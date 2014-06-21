using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FollowerManager : MonoBehaviour
{
	// ==================================================
	// Variables
	// ==================================================

	public GameObject mPlayer;

	public List<GameObject> mFollowers;

	// ==================================================
	// Methods
	// ==================================================

	public void addFollower (GameObject followerObject)
	{
		Debug.Log (mFollowers.Count);
		followerObject.GetComponent<FollowerMovementScript> ().mTargetPosition = mPlayer.transform.position;
		mFollowers.Add (followerObject);
		Debug.Log (mFollowers.Count);
	}

	// ====================
	// Lifecycle Methods
	// ====================

	protected void Start ()
	{
		mFollowers = new List<GameObject> ();
		Debug.Log (mFollowers.Count);
	}

	protected void Update ()
	{

	}
}
