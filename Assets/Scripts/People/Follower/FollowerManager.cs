using UnityEngine;
using System.Collections;

public class FollowerManager : MonoBehaviour
{

	GameObject[] mFollowers;

	// Use this for initialization
	void Start ()
	{
		mFollowers = GameObject.FindGameObjectsWithTag ("follower");
		Debug.Log (mFollowers.Length);
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
