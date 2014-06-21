using UnityEngine;
using System.Collections;

public class PressurePlate : MonoBehaviour {

	public bool activated = false;

	void OnCollisionEnter2D (Collision2D collision)
	{
		activated = true;
	}

	void OnCollisionExit2D (Collision2D collision)
	{
		activated = false;
	}
}
