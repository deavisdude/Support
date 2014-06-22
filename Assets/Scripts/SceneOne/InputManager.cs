using UnityEngine;
using System.Collections;

public class InputManager : SPSUGameJamScript
{
	public GameObject leftSelector;
	public GameObject rightSelector;
	public GameObject leftRightArrows;

	void Start ()
	{
		base.Start ();
		rightSelector.renderer.material.shader = Shader.Find ("Transparent/Diffuse");
		leftSelector.renderer.material.shader = Shader.Find ("Transparent/Diffuse");

		Color leftColor = leftSelector.renderer.material.color;
		leftColor.a = 0.0f;
		leftSelector.renderer.material.color = leftColor;

		Color rightColor = rightSelector.renderer.material.color;
		rightColor.a = 0.0f;
		rightSelector.renderer.material.color = rightColor;
	}

	void Update ()
	{
		if (Input.GetAxis ("Horizontal") > 0) {
			showRight ();
		} else if (Input.GetAxis ("Horizontal") < 0) {
			showLeft ();
		} else if (Input.GetButton ("Jump")) {
			if (leftSelector.renderer.material.color.a == 1f) {
				PlayerMovement.isBoy = true;
			} else if (rightSelector.renderer.material.color.a == 1f) {
				PlayerMovement.isBoy = false;
			} else {
				return;
			}

			Application.LoadLevel ("lvl1");
			Obstacle.enemyIsBoy = Random.Range (0, 2) == 0;
			audioManager.playMenuSound ();
			audioManager.startGameMusic ();
		}

		if(Input.GetKeyDown(KeyCode.Escape))
			Application.Quit();
	}
	
	private void showRight ()
	{
		Color rightColor = rightSelector.renderer.material.color;
		rightColor.a = 1.0f;
		rightSelector.renderer.material.color = rightColor;

		Color leftColor = leftSelector.renderer.material.color;
		leftColor.a = 0.0f;
		leftSelector.renderer.material.color = leftColor;
		hideArrows ();
	}
	
	private void showLeft ()
	{
		Color color = rightSelector.renderer.material.color;
		color.a = 0.0f;
		rightSelector.renderer.material.color = color;
		
		Color leftColor = leftSelector.renderer.material.color;
		leftColor.a = 1.0f;
		leftSelector.renderer.material.color = leftColor;
		hideArrows ();
	}

	private void hideArrows ()
	{
		Color arrowColor = leftRightArrows.renderer.material.color;
		arrowColor.a = 0.0f;
		leftRightArrows.renderer.material.color = arrowColor;
	}
}
