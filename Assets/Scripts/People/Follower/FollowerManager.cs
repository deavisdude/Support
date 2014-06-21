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
