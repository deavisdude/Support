using UnityEngine;
using System.Collections;

public class SPSUGameJamScript : MonoBehaviour
{
	// ==================================================
	// Variables
	// ==================================================

	private GameObject audioManagerGameObject;
	private SPSUAudioManager _audioManager;
	protected SPSUAudioManager audioManager
	{
		get{
			if(audioManagerGameObject == null)
				audioManagerGameObject = GameObject.FindGameObjectWithTag ("audioManager");

			if (_audioManager == null) {
				_audioManager = audioManagerGameObject.GetComponent<SPSUAudioManager> ();
			}

			return _audioManager;
		}
	}

	// ==================================================
	// Methods
	// ==================================================

	// =========================
	// Lifecycle Methods
	// =========================

	public void Start ()
	{
		audioManagerGameObject = GameObject.FindGameObjectWithTag ("audioManager");

		if (audioManagerGameObject != null) {
			_audioManager = audioManagerGameObject.GetComponent<SPSUAudioManager> ();
		}
	}
}
