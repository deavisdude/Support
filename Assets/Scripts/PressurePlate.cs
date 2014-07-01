using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class PressurePlate : SPSUGameJamScript
{
	public bool activated = false;
	public SpriteRenderer spriteRender;
	private Vector3 defaultScale;
	private Vector3 activatedScale;
	public int collisionCount = 0;
	public Transform sprite;

	void Awake ()
	{
		if (spriteRender == null)
			spriteRender = GetComponent<SpriteRenderer> ();

		defaultScale = sprite.localScale;
		activatedScale = new Vector3(defaultScale.x, defaultScale.y * 0.75f, defaultScale.z);
	}


	void OnTriggerEnter2D (Collider2D collider)
	{
		if ((collider.gameObject.layer == LayerMask.NameToLayer ("player") 
		     || collider.gameObject.layer == LayerMask.NameToLayer ("followingPeople")) 
		     && collider.GetType() == typeof(CircleCollider2D))
		{
			collisionCount++;

			if (!PressurePlateManager.DontChangePlateState() && !activated && collisionCount == 1) 
			{
				activated = true;
				sprite.localScale = activatedScale;

				if(collider.gameObject.layer == LayerMask.NameToLayer ("followingPeople"))
					audioManager.playPressurePlateActivatedSound (0.5f);
				else if(collider.gameObject.layer == LayerMask.NameToLayer ("player"))
				{
					audioManager.playPressurePlateActivatedSound ();
				}

				spriteRender.color = Color.green;
				StopAllCoroutines ();
			}
		}
	}

	void OnTriggerExit2D (Collider2D collider)
	{
		if ((collider.gameObject.layer == LayerMask.NameToLayer ("player") 
		     || collider.gameObject.layer == LayerMask.NameToLayer ("followingPeople"))
		     && collider.GetType() == typeof(CircleCollider2D))
		{
			if(collisionCount > 0)
				collisionCount--;

			if (!PressurePlateManager.DontChangePlateState() && collisionCount == 0) {

				DeactivateNow (false);

				if(collider.gameObject.layer == LayerMask.NameToLayer ("followingPeople"))
					audioManager.playPressurePlateDeactivedSound (0.5f);
				else if(collider.gameObject.layer == LayerMask.NameToLayer ("player"))
					audioManager.playPressurePlateDeactivedSound ();
			}
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
		HOTween.To(sprite, deactivateTimeLength, "localScale", defaultScale);
	}

	// with sound called by the timer tween OnComplete
	private void DeactivateNow()
	{
		DeactivateNow(true);
	}

	// without sound called by OnTriggerExit, because it plays a sound at a volume depending on whether it's the player or a NPC
	private void DeactivateNow (bool playSound)
	{
		activated = false;
		spriteRender.color = Color.white;
		sprite.localScale = defaultScale;

		if(playSound)
			audioManager.playPressurePlateDeactivedSound(2);
	}
}
