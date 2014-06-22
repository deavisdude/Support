using UnityEngine;
using System.Collections;

public class BringParticleSystemToFrontScript : MonoBehaviour
{
	void Start ()
	{
		particleSystem.renderer.sortingLayerName = "rain";
	}
}
