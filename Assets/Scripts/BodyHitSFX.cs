using UnityEngine;
using System.Collections;

public class BodyHitSFX : SPSUGameJamScript {

	void OnCollisionEnter2D (Collision2D collision)
	{
		audioManager.playBodyHitSound();
	}
}
