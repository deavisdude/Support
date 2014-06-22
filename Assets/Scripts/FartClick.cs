using UnityEngine;
using System.Collections;

public class FartClick : MonoBehaviour
{

	public AudioClip fart;
	public GameObject PlayHere;

	void Start ()
	{
		PlayHere.GetComponent<AudioSource> ().clip = fart;
	}

	void OnMouseOver ()
	{
		if (Input.GetMouseButtonDown (0)) {
			PlayHere.GetComponent<AudioSource> ().PlayOneShot (fart);
		}
	}
}
