using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	public bool timed = false;
	public float growBackWaitTime = 10;
	
	public void Shrink()
	{
		// shrink the obstacle

		if(timed)
			StartCoroutine(GrowBack());
	}

	private IEnumerator GrowBack()
	{
		yield return new WaitForSeconds(growBackWaitTime);

		// grow the obstace back to normal size
	}
}
