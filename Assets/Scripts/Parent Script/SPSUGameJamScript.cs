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

	void Start ()
	{
		GameObject audioManagerGameObject = GameObject.FindGameObjectWithTag ("audioManager");
		audioManager = audioManagerGameObject.GetComponent<SPSUAudioManager> ();
	}
	
	void Update ()
	{
	
	}
}
