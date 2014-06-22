using UnityEngine;
using System.Collections;

public class Exit : SPSUGameJamScript
{
	public static Color[] playerColors = new Color[4];
	public static Color[] enemyColors = new Color[4];


	void Awake() {

		playerColors[0] = new Color (120f / 255f, 120f / 255f, 240f / 255f);
		playerColors[1] = new Color (80f / 255f, 200f / 255f, 240f / 255f);
		playerColors[2] = new Color (80f / 255f, 240f / 255f, 200f / 255f);
		playerColors[3] = new Color (80f / 255f, 240f / 255f, 80f / 255f);

		enemyColors[0] = new Color (255f / 255f, 50f / 255f, 50f / 255f);
		enemyColors[1] = new Color (220f / 255f, 120f / 255f, 120f / 255f);
		enemyColors[2] = playerColors[1];
		enemyColors[3] = playerColors[0];

	}


	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer ("player")) {
			if (Application.loadedLevel + 1 < Application.levelCount) {
				int newLevel = Application.loadedLevel + 1;


				switch (newLevel) {
				case 2:
					audioManager.incrementTrackIndex ();
					break;
					
				case 3:
					audioManager.incrementTrackIndex ();
					break;
					
				case 4:
					audioManager.playBirdsAndBeesSound ();
					break;
				}

				Application.LoadLevel (newLevel);
			} else {
				Application.LoadLevel (0);
			}
		}
	}

	public static int GetCurrentLevelColorIndex()
	{
		return Application.loadedLevel - 1;
	}

	public static int GetNextLevelColorIndex()
	{
		return Application.loadedLevel;
	}
}
