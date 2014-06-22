using UnityEngine;
using System.Collections;

public class PressurePlateManager : MonoBehaviour {

	public PressurePlate[] pressurePlates;
	private static bool _allPlatesActive = false;
	public static bool allPlatesActive
	{
		get { return _allPlatesActive; }
	}
	public Obstacle obstacle;

	// Update is called once per frame
	void Update () {

		if(!allPlatesActive)
		{
			_allPlatesActive = true;
			foreach(PressurePlate plate in pressurePlates)
			{
				if(!plate.activated)
				{
					_allPlatesActive = false;
					break;
				}
			}

			if(_allPlatesActive)
			{
				// tell obstacle to shrink and start timer
				obstacle.Shrink();

				if(obstacle.timed)
				{
					for (int i = (pressurePlates.Length - 1); i >= 0; i--)
					{
						float plateDeactivationTime = obstacle.growBackWaitTime / pressurePlates.Length;
						pressurePlates[i].DeactivateTimed(plateDeactivationTime * (pressurePlates.Length - i), plateDeactivationTime);
						StartCoroutine(WaitAndDeactivate(obstacle.growBackWaitTime));
					}
				}
			}
		}
	}

	private IEnumerator WaitAndDeactivate(float wait)
	{
		yield return new WaitForSeconds(wait);
		_allPlatesActive = false;
	}
}
