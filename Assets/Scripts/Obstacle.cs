using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class Obstacle : MonoBehaviour {

	public bool timed = false;
	public float growBackWaitTime = 10;
	private Vector3 startScale;
	private float shrinkAmount = 0.5f;
	public Transform exitTransform;
	public SpriteRenderer[] spriteRenderers;

	void Awake()
	{
		startScale = transform.localScale;
	}
	
	public void Shrink()
	{
		// shrink the obstacle
		TweenParms tween = new TweenParms().Prop("localScale", new Vector3(startScale.x - 0.5f, startScale.y - 0.5f, startScale.z - 0.5f)).OnComplete(GoToExit);
		HOTween.To(transform, 1, tween);

		//animation.Play("Shrink");
		if(timed)
			StartCoroutine(GrowBack());
	}

	private void GoToExit()
	{
		TweenParms tween = new TweenParms().Prop("position", exitTransform.position).OnComplete(Fade);
		HOTween.To(transform, 1, tween);
	}

	private void Fade()
	{
		foreach(SpriteRenderer sprite in spriteRenderers)
			HOTween.To(sprite, 1, "color", new Color(1,1,1,0));
	}

	private IEnumerator GrowBack()
	{
		yield return new WaitForSeconds(growBackWaitTime);

		// grow the obstace back to normal size
		HOTween.To(transform, 1, "localScale", startScale);
	}
}
