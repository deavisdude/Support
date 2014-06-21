using UnityEngine;
using System.Collections;

public class PressurePlateManager : MonoBehaviour {

	public PressurePlate[] pressurePlates;
	private bool allPlatesActive = false;

	// Update is called once per frame
	void Update () {

		allPlatesActive = true;
		foreach(PressurePlate plate in pressurePlates)
		{
			if(!plate.activated)
			{
				allPlatesActive = false;
				break;
			}
		}

		if(allPlatesActive)
		{
			// tell obstacle to shrink and start timer
		}
	}
}
