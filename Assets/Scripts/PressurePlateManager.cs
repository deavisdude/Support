using UnityEngine;
using System.Collections;

public class PressurePlateManager : MonoBehaviour
{

	public PressurePlate[] pressurePlates;

	public CreditsScript creditsScript;

	private bool _allPlatesActive = false;
	public Obstacle obstacle;

	private static bool waitingToStartTimer = false;
	private static bool _timerStarted = false;
	public static bool timerStarted {
		get { return _timerStarted; }
	}

	void OnLevelWasLoaded (int level)
	{
		_allPlatesActive = false;
		waitingToStartTimer = false;
		_timerStarted = false;
	}

	public static bool DontChangePlateState()
	{
		return waitingToStartTimer || timerStarted;
	}

	// Update is called once per frame
	void Update ()
	{
		_allPlatesActive = true;
		foreach (PressurePlate plate in pressurePlates) 
		{
			if (!plate.activated || plate.collisionCount == 0) {
				_allPlatesActive = false;
				break;
			}
		}

		if (_allPlatesActive && !waitingToStartTimer && !timerStarted) 
		{
			if (Application.loadedLevel != 4) {
				// tell obstacle to shrink
				obstacle.Shrink ();

				waitingToStartTimer = true;
			}
			else  // level 4
			{
				if (creditsScript != null) {
					creditsScript.startFadingIn ();
				}
			}
		}

		if(!_allPlatesActive && waitingToStartTimer && !timerStarted && obstacle.timed)
		{
			_timerStarted = true;
			waitingToStartTimer = false;

			obstacle.StartGrowBackTimer();

			for (int i = (pressurePlates.Length - 1); i >= 0; i--) {
				float plateDeactivationTime = obstacle.growBackWaitTime / pressurePlates.Length;
				pressurePlates [i].DeactivateTimed (plateDeactivationTime * (pressurePlates.Length - (i + 1)), plateDeactivationTime);
			}
			StartCoroutine (WaitAndDeactivate (obstacle.growBackWaitTime));
		}
	}

	private IEnumerator WaitAndDeactivate (float wait)
	{
		yield return new WaitForSeconds (wait);
		waitingToStartTimer = false;
		_timerStarted = false;
	}
}
