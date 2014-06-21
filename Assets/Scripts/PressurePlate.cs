using UnityEngine;
using System.Collections;
using Holoville.HOTween;

public class PressurePlate : MonoBehaviour {

	public bool activated = false;
	public SpriteRenderer spriteRender;

	void Awake ()
	{
		if(spriteRender == null)
			spriteRender = GetComponent<SpriteRenderer>();
	}


	void OnTriggerEnter2D (Collider2D collider)
	{
		if(collider.gameObject.layer == LayerMask.NameToLayer("player") || collider.gameObject.layer == LayerMask.NameToLayer("followingPeople"))
		{
			activated = true;
			spriteRender.color = Color.green;
			StopAllCoroutines();
		}
	}

	void OnTriggerExit2D (Collider2D collider)
	{
		if(!PressurePlateManager.allPlatesActive)
			DeactivateNow();
	}

	public void DeactivateTimed(float waitTime, float deactivateTimeLength)
	{
		StartCoroutine(WaitAndDeactivate(waitTime, deactivateTimeLength));
	}

	private IEnumerator WaitAndDeactivate(float waitTime, float deactivateTimeLength)
	{
		yield return new WaitForSeconds(waitTime);

		TweenParms tween = new TweenParms().Prop("color", Color.white).OnComplete(DeactivateNow);
		HOTween.To(spriteRender, deactivateTimeLength, tween);
	}

	private void DeactivateNow()
	{
		activated = false;
		spriteRender.color = Color.white;
	}
}
