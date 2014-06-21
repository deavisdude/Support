using UnityEngine;
using System.Collections;

public class Exit : MonoBehaviour {

	void OnTriggerEnter2D (Collider2D other)
	{
		if(Application.loadedLevel + 1 < Application.levelCount)
			Application.LoadLevel(Application.loadedLevel + 1);
		else
			Application.LoadLevel(0);
	}
}
