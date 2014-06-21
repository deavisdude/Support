using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class Obstacle : MonoBehaviour {

	public bool timed = false;
	public float growBackWaitTime = 10;
	private Vector3 startScale;
	private float shrinkAmount = 0.5f;

	void Awake()
	{
		startScale = transform.localScale;
	}
	
	public void Shrink()
	{
		// shrink the obstacle
		HOTween.To(transform, 1, "localScale", startScale * .5f);

		//animation.Play("Shrink");
		if(timed)
			StartCoroutine(GrowBack());
	}

	private IEnumerator GrowBack()
	{
		yield return new WaitForSeconds(growBackWaitTime);

		// grow the obstace back to normal size
		HOTween.To(transform, 1, "localScale", startScale);
	}
}
