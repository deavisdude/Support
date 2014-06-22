using UnityEngine;
using System.Collections;

public class PlayAsBoy : SPSUGameJamScript
{

	void OnMouseOver ()
	{
		if (Input.GetMouseButtonDown (0)) {
			Application.LoadLevel ("lvl1");
			PlayerMovement.isBoy = true;
			Obstacle.enemyIsBoy = Random.Range(0,2) == 0;
			audioManager.playMenuSound ();
			audioManager.startGameMusic ();
		}
	}
}
