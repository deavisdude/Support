using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BodyHitSFX : SPSUGameJamScript
{

	List<GameObject> collidedGameObjects = new List<GameObject> ();

	void OnCollisionEnter2D (Collision2D collision)
	{
		if (!collidedGameObjects.Contains (collision.gameObject)) {
			audioManager.playBodyHitSound ();
			collidedGameObjects.Add (collision.gameObject);
		}
	}

	void OnCollisionExit2D (Collision2D collision)
	{
		collidedGameObjects.Remove (collision.gameObject);
	}
}
