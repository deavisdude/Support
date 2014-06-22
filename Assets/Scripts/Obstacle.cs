using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class Obstacle : MonoBehaviour {

	public bool timed = false;
	public float growBackWaitTime = 10;
	private Vector3 startScale;
	private Vector3 startPosition;
	private float shrinkAmount = 0.5f;
	public Transform exitTransform;
	public SpriteRenderer[] spriteRenderers;

	void Awake()
	{
		startScale = transform.localScale;
		startPosition = transform.position;
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

		foreach(SpriteRenderer sprite in spriteRenderers)
			HOTween.To(sprite, 1, "color", new Color(1,1,1,1));

		yield return new WaitForSeconds(1);

		HOTween.To(transform, 1, "position", startPosition);

		yield return new WaitForSeconds(1);

		// grow the obstace back to normal size
		HOTween.To(transform, 1, "localScale", startScale);
	}
}
