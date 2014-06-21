using UnityEngine;
using System.Collections;

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
		activated = true;
		spriteRender.color = Color.green;
		StopAllCoroutines();
	}

	void OnTriggerExit2D (Collider2D collider)
	{
		activated = false;
		spriteRender.color = Color.white;
	}

	public void Deactivate(float waitTime, float deactivateTimeLength)
	{
		StartCoroutine(WaitAndDeactivate(waitTime, deactivateTimeLength));
	}

	private IEnumerator WaitAndDeactivate(float waitTime, float deactivateTimeLength)
	{
		yield return new WaitForSeconds(waitTime);

		// start deactivation
	}
}
