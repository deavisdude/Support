using UnityEngine;
using System.Collections;

public class CreditsScript : SPSUGameJamScript
{
	private const float FADE_SPEED = .25f;

	private bool mIsFadingIn = false;
	private bool mInputAllowed = false;

	public void startFadingIn ()
	{
		mIsFadingIn = true;
	}

	public void Update ()
	{
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
				Debug.Log ("quit game");
			}
		}
	}
}
