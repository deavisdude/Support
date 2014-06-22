using UnityEngine;
using System.Collections;

public class PlayAsGirl : SPSUGameJamScript
{

	void OnMouseOver ()
	{
		if (Input.GetMouseButtonDown (0)) {
			Application.LoadLevel ("lvl1");
			PlayerMovement.isBoy = false;
			Obstacle.enemyIsBoy = Random.Range(0,2) == 0;
			audioManager.playMenuSound ();
			audioManager.startGameMusic ();
		}
	}
}
