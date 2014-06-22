﻿using UnityEngine;
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

	public static bool enemyIsBoy;
	public Sprite boyClothes;
	public Sprite girlClothes;

	public Collider2D myCollider2D;

	void Awake()
	{
		startScale = transform.localScale;
		startPosition = transform.position;

		if(myCollider2D == null)
			myCollider2D = GetComponentInChildren<Collider2D>();
	}

	void Start ()
	{
		if (enemyIsBoy) {
			spriteRenderers[1].sprite = boyClothes;
		} else {
			spriteRenderers[1].sprite = girlClothes;
		}
	}
	
	public void Shrink()
	{
		// shrink the obstacle
		TweenParms tween = new TweenParms().Prop("localScale", new Vector3(startScale.x - 1, startScale.y - 1, startScale.z - 1)).OnComplete(GoToExit);
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
		myCollider2D.enabled = false;

		foreach(SpriteRenderer sprite in spriteRenderers)
			HOTween.To(sprite, 1, "color", new Color(1,1,1,0));
	}

	private IEnumerator GrowBack()
	{
		yield return new WaitForSeconds(growBackWaitTime);

		myCollider2D.enabled = true;

		foreach(SpriteRenderer sprite in spriteRenderers)
			HOTween.To(sprite, 1, "color", new Color(1,1,1,1));

		yield return new WaitForSeconds(1);

		HOTween.To(transform, 1, "position", startPosition);

		yield return new WaitForSeconds(1);

		// grow the obstace back to normal size
		HOTween.To(transform, 1, "localScale", startScale);
	}
}
