using UnityEngine;
using System.Collections;

public class FollowerManager : MonoBehaviour
{

	ArrayList mFollowers = new ArrayList ();

	public void testMethod (GameObject followerObject)
	{
		if (!mFollowers.Contains (followerObject)) {
			mFollowers.Add (followerObject);
		}
	}

	// Use this for initialization
	void Start ()
	{

	}

	void Update ()
	{
	
	}
}
