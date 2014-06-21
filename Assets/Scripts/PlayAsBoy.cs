using UnityEngine;
using System.Collections;

public class PlayAsBoy : MonoBehaviour {

	void OnMouseOver(){
		if(Input.GetMouseButtonDown(0)){
			Application.LoadLevel("lvl1");
			PlayerMovement.isBoy = true;
		}
	}
}
