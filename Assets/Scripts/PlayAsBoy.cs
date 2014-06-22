using UnityEngine;
using System.Collections;

public class PlayAsBoy : SPSUGameJamScript
{

	void OnMouseOver ()
	{
		if (Input.GetMouseButtonDown (0)) {
			Application.LoadLevel ("lvl1");
			PlayerMovement.isBoy = true;
			audioManager.playMenuSound ();
			audioManager.startGameMusic ();
		}
	}
}
