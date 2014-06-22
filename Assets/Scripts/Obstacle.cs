﻿using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class Obstacle : SPSUGameJamScript
{
	// ==================================================
	// Variables
	// ==================================================

	public static bool enemyIsBoy;
	public static Color enemyColor = Exit.enemyLevelOne;

	public bool timed = false;

	public Collider2D myCollider2D;

	public float growBackWaitTime = 10;

	public Sprite boyClothes;
	public Sprite girlClothes;

	public SpriteRenderer[] spriteRenderers;

	public Transform exitTransform;

	private float shrinkAmount = 0.5f;

	private Vector3 startScale;
	private Vector3 startPosition;

	// ==================================================
	// Methods
	// ==================================================

	public void Shrink ()
	{
		TweenParms tween = new TweenParms ().Prop ("localScale", new Vector3 (startScale.x - 1, startScale.y - 1, startScale.z - 1)).OnComplete (GoToExit);
		HOTween.To (transform, 1, tween);
		audioManager.playEvilLaughSound ();

		if (timed) {
			StartCoroutine (GrowBack ()); 
		}
	}

	private IEnumerator GrowBack ()
	{
		yield return new WaitForSeconds (growBackWaitTime);
		
		myCollider2D.enabled = true;
		
		foreach (SpriteRenderer sprite in spriteRenderers)
			HOTween.To (sprite, 1, "color", new Color (1, 1, 1, 1));
		
		yield return new WaitForSeconds (1);
		
		HOTween.To (transform, 1, "position", startPosition);
		
		yield return new WaitForSeconds (1);
		
		// grow the obstace back to normal size
		HOTween.To (transform, 1, "localScale", startScale);
	}

	private void GoToExit ()
	{
		TweenParms tween = new TweenParms ().Prop ("position", exitTransform.position).OnComplete (Fade);
		HOTween.To (transform, 1, tween);
	}

	private void Fade ()
	{
		myCollider2D.enabled = false;

		foreach (SpriteRenderer sprite in spriteRenderers) {
			HOTween.To (sprite, 1, "color", new Color (1, 1, 1, 0));
		}
	}

	// =========================
	// Lifecycle Methods
	// =========================

	void Awake ()
	{
		startScale = transform.localScale;
		startPosition = transform.position;
		
		if (myCollider2D == null) {
			myCollider2D = GetComponentInChildren<Collider2D> ();
		}
	}
	
	void Start ()
	{
		base.Start ();
		if (enemyIsBoy) {
			spriteRenderers [1].sprite = boyClothes;
		} else {
			spriteRenderers [1].sprite = girlClothes;
		}

		GetComponentInChildren<SpriteRenderer> ().material.color = enemyColor;
	}

}
