using UnityEngine;
using System.Collections;

public class PlayAsGirl : MonoBehaviour {

	void OnMouseOver(){
		if(Input.GetMouseButtonDown(0)){
			Application.LoadLevel("lvl1");
			PlayerMovement.isBoy = false;
		}
	}
}
