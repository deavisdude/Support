using UnityEngine;
using System.Collections;

public class ExitOnClick : MonoBehaviour {

	void OnMouseOver(){
		if(Input.GetMouseButtonDown(0)){
			Application.Quit();
		}
	}
}
