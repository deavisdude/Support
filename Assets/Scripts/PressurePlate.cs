using UnityEngine;
using System.Collections;

public class PressurePlate : MonoBehaviour {

	public bool activated = false;

	void OnCollisionEnter2D (Collision2D collision)
	{
		activated = true;
		StopAllCoroutines();
	}

	void OnCollisionExit2D (Collision2D collision)
	{
		activated = false;
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
