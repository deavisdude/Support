using UnityEngine;
using System.Collections;

public class InputManager : SPSUGameJamScript
{
	public SpriteRenderer leftSelector;
	public SpriteRenderer rightSelector;
	public GameObject leftRightArrows;

	new void Start ()
	{
		base.Start ();

		Color leftColor = leftSelector.color;
		leftColor.a = 0.0f;
		leftSelector.color = leftColor;

		Color rightColor = rightSelector.color;
		rightColor.a = 0.0f;
		rightSelector.color = rightColor;
	}

	void Update ()
	{
		if (Input.GetAxis ("Horizontal") > 0) {
			showRight ();
		} else if (Input.GetAxis ("Horizontal") < 0) {
			showLeft ();
		} else if (Input.GetButton ("Jump") || Input.GetKey (KeyCode.Return)) {
			if (leftSelector.color.a == 1f) {
				PlayerMovement.isBoy = true;
			} else if (rightSelector.color.a == 1f) {
				PlayerMovement.isBoy = false;
			} else {
				return;
			}

			Obstacle.enemyIsBoy = Random.Range (0, 2) == 0;
			audioManager.playLevelUpSound();
			audioManager.startGameMusic ();

			Application.LoadLevel ("lvl1");
		}

		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
	}
	
	private void showRight ()
	{
		Color rightColor = rightSelector.color;

		if(rightColor.a == 0)
			audioManager.playMenuSound();

		rightSelector.color = Color.black;

		Color leftColor = leftSelector.color;
		leftColor.a = 0.0f;
		leftSelector.color = leftColor;
		hideArrows ();
	}
	
	private void showLeft ()
	{
		Color rightColor = rightSelector.color;
		rightColor.a = 0.0f;
		rightSelector.color = rightColor;
		
		Color leftColor = leftSelector.color;

		if(leftColor.a == 0)
			audioManager.playMenuSound();

		leftSelector.color = Color.black;
		hideArrows ();
	}

	private void hideArrows ()
	{
		leftRightArrows.SetActive(false);
	}
}
