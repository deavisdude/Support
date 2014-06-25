using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class PressurePlate : SPSUGameJamScript
{
	public bool activated = false;
	public SpriteRenderer spriteRender;

	void Awake ()
	{
		if (spriteRender == null)
			spriteRender = GetComponent<SpriteRenderer> ();
	}


	void OnTriggerEnter2D (Collider2D collider)
	{
		if (!PressurePlateManager.allPlatesActive) {	
			if ((collider.gameObject.layer == LayerMask.NameToLayer ("player") 
				|| collider.gameObject.layer == LayerMask.NameToLayer ("followingPeople"))
				&& !activated) {
				activated = true;

				if(collider.gameObject.layer == LayerMask.NameToLayer ("followingPeople"))
					audioManager.playPressurePlateActivatedSound (0.5f);
				else
					audioManager.playPressurePlateActivatedSound ();

				spriteRender.color = Color.green;
				StopAllCoroutines ();
			}
		}
	}

	void OnTriggerStay2D (Collider2D collider)
	{
		if (!PressurePlateManager.allPlatesActive) {
			if ((collider.gameObject.layer == LayerMask.NameToLayer ("player") 
			     || collider.gameObject.layer == LayerMask.NameToLayer ("followingPeople")))
		    {
				activated = true;
				spriteRender.color = Color.green;
			}
		}
	}

	void OnTriggerExit2D (Collider2D collider)
	{
		if (!PressurePlateManager.allPlatesActive && activated) {	
			DeactivateNow (false);

			if(collider.gameObject.layer == LayerMask.NameToLayer ("followingPeople"))
				audioManager.playPressurePlateDeactivedSound (0.5f);
			else
				audioManager.playPressurePlateDeactivedSound ();
		}
	}

	public void DeactivateTimed (float waitTime, float deactivateTimeLength)
	{
		StartCoroutine (WaitAndDeactivate (waitTime, deactivateTimeLength));
	}

	private IEnumerator WaitAndDeactivate (float waitTime, float deactivateTimeLength)
	{
		yield return new WaitForSeconds (waitTime);

		TweenParms tween = new TweenParms ().Prop ("color", Color.white).OnComplete (DeactivateNow);
		HOTween.To (spriteRender, deactivateTimeLength, tween);
	}

	private void DeactivateNow()
	{
		DeactivateNow(true);
	}

	private void DeactivateNow (bool playSound)
	{
		activated = false;
		spriteRender.color = Color.white;

		if(playSound)
			audioManager.playPressurePlateDeactivedSound(2);
	}
}
