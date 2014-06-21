using UnityEngine;
using System.Collections;

public class GenerateScreenBorders : MonoBehaviour {
	
	public GameObject borderCubePrefab;
	public Camera cam;
	private int zVal = 10;
	
	// Use this for initialization
	void Start() {
		
		if(cam == null)
				cam = Camera.main;
		
		Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0,0,zVal));
		Vector3 bottomRight = cam.ViewportToWorldPoint(new Vector3(1,0,zVal));
		Vector3 topLeft = cam.ViewportToWorldPoint(new Vector3(0,1,zVal));
		Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1,1,zVal));
		
		GameObject leftBorder = GameObject.Instantiate(borderCubePrefab, new Vector3(bottomLeft.x - 0.5f, ((topLeft.y + bottomLeft.y) * 0.5f), bottomLeft.z), Quaternion.Euler(Vector3.zero)) as GameObject;
		leftBorder.name = "left screen border collision cube";
		Vector3 tempScale = leftBorder.transform.localScale;
		tempScale.y = topLeft.y - bottomLeft.y;
		leftBorder.transform.localScale = tempScale;
		leftBorder.transform.parent = transform;
		
		GameObject rightBorder = GameObject.Instantiate(borderCubePrefab, new Vector3(bottomRight.x + 0.5f, ((topRight.y + bottomRight.y) * 0.5f), bottomRight.z), Quaternion.Euler(Vector3.zero)) as GameObject;
		rightBorder.name = "right screen border collision cube";
		tempScale = rightBorder.transform.localScale;
		tempScale.y = topRight.y - bottomRight.y;
		rightBorder.transform.localScale = tempScale;
		rightBorder.transform.parent = transform;
		
		GameObject bottomBorder = GameObject.Instantiate(borderCubePrefab, new Vector3(((bottomLeft.x + bottomRight.x) * 0.5f), bottomLeft.y - 0.5f, bottomLeft.z), Quaternion.Euler(Vector3.zero)) as GameObject;
		bottomBorder.name = "bottom screen border collision cube";
		tempScale = bottomBorder.transform.localScale;
		tempScale.x = bottomRight.x - bottomLeft.x;
		bottomBorder.transform.localScale = tempScale;
		bottomBorder.transform.parent = transform;
		
		GameObject topBorder = GameObject.Instantiate(borderCubePrefab, new Vector3(((topLeft.x + topRight.x) * 0.5f), topLeft.y + 0.5f, topLeft.z), Quaternion.Euler(Vector3.zero)) as GameObject;
		topBorder.name = "top screen border collision cube";
		tempScale = topBorder.transform.localScale;
		tempScale.x = topRight.x - topLeft.x;
		topBorder.transform.localScale = tempScale;
		topBorder.transform.parent = transform;
	}
}
