using UnityEngine;
using System.Collections;

public class Exit : SPSUGameJamScript
{
	public static Color playerLevelOne = new Color (120f / 255f, 120f / 255f, 240f / 255f);
	public static Color playerLevelTwo = new Color (80f / 255f, 200f / 255f, 240f / 255f);
	public static Color playerLevelThree = new Color (80f / 255f, 240f / 255f, 200f / 255f);
	public static Color playerLevelFour = new Color (80f / 255f, 240f / 255f, 80f / 255f);

	public static Color enemyLevelOne = new Color (255f / 255f, 50f / 255f, 50f / 255f);
	public static Color enemyLevelTwo = new Color (220f / 255f, 120f / 255f, 120f / 255f);
	public static Color enemyLevelThree = Exit.playerLevelTwo;
	public static Color enemyLevelFour = Exit.playerLevelOne;

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer ("player")) {
			if (Application.loadedLevel + 1 < Application.levelCount) {
				int newLevel = Application.loadedLevel + 1;

				switch (newLevel) {
				case 2:
					audioManager.incrementTrackIndex ();
					PlayerMovement.currentColor = Exit.playerLevelTwo;
					Obstacle.enemyColor = Exit.enemyLevelTwo;
					break;
					
				case 3:
					audioManager.incrementTrackIndex ();
					PlayerMovement.currentColor = Exit.playerLevelThree;
					Obstacle.enemyColor = Exit.enemyLevelThree;
					break;
					
				case 4:
					PlayerMovement.currentColor = Exit.playerLevelFour;
					Obstacle.enemyColor = Exit.enemyLevelFour;
					audioManager.playBirdsAndBeesSound ();
					break;
				}

				Application.LoadLevel (newLevel);
			} else {
				Application.LoadLevel (0);
			}
		}
	}
}
