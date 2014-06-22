using UnityEngine;
using System.Collections;

public class SPSUGameJamScript : MonoBehaviour
{
	// ==================================================
	// Variables
	// ==================================================

	protected SPSUAudioManager audioManager;

	// ==================================================
	// Methods
	// ==================================================

	// =========================
	// Lifecycle Methods
	// =========================

	public void Start ()
	{
		GameObject audioManagerGameObject = GameObject.FindGameObjectWithTag ("audioManager");

		if (audioManagerGameObject != null) {
			audioManager = audioManagerGameObject.GetComponent<SPSUAudioManager> ();
		}
	}
	
	void Update ()
	{
	
	}
}
