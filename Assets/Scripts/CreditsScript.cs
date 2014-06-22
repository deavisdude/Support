using UnityEngine;
using System.Collections;

public class CreditsScript : SPSUGameJamScript
{
	private const float FADE_SPEED = .25f;

	public Color testColor;

	public bool mIsFadingIn = false;
	private bool mInputAllowed = false;

	public void startFadingIn ()
	{
		mIsFadingIn = true;
		audioManager.playCreditsMusic ();
	}

	public void Start ()
	{
		base.Start ();
		Color color = renderer.material.color;
		color.a = 0;
		renderer.material.color = color;
	}

	public void Update ()
	{
		testColor = renderer.material.color;
		if (mIsFadingIn) {
			Color currentColor = renderer.material.color;
			currentColor.a += FADE_SPEED * Time.deltaTime;
			renderer.material.color = currentColor;

			if (renderer.material.color.a >= 1f) {
				mIsFadingIn = false;
				mInputAllowed = true;
			}
		}

		if (mInputAllowed) {
			if (Input.anyKey) {
				audioManager.fadeCreditsMusicOut ();
				Application.LoadLevel (0);
			}
		}
	}
}
