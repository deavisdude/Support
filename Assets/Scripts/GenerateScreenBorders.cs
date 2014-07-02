using UnityEngine;
using System.Collections;

public class GenerateScreenBorders : MonoBehaviour {
	
	public GameObject borderCubePrefab;
	public Camera cam;
	private int zVal = 10;
	private float screenRatio;

	private Transform leftBorder;
	private Transform rightBorder;
	private Transform bottomBorder;
	private Transform topBorder;

	private float GetScreenRatio()
	{
		return (float)Screen.width/(float)Screen.height;
	}
	
	// Use this for initialization
	void Start() {

		screenRatio = GetScreenRatio();

		if(cam == null)
				cam = Camera.main;

		CalculateColliders();
	}

	private void CalculateColliders()
	{
		Vector3 bottomLeft = cam.ViewportToWorldPoint(new Vector3(0,0,zVal));
		Vector3 bottomRight = cam.ViewportToWorldPoint(new Vector3(1,0,zVal));
		Vector3 topLeft = cam.ViewportToWorldPoint(new Vector3(0,1,zVal));
		Vector3 topRight = cam.ViewportToWorldPoint(new Vector3(1,1,zVal));

		if(leftBorder == null)
		{
			GameObject leftBorderGO = GameObject.Instantiate(borderCubePrefab) as GameObject;
			leftBorderGO.name = "left screen border collision cube";
			leftBorder = leftBorderGO.transform;
			leftBorder.parent = transform;
		}
		leftBorder.position = new Vector3(bottomLeft.x - 0.5f, ((topLeft.y + bottomLeft.y) * 0.5f), bottomLeft.z);
		Vector3 tempScale = leftBorder.localScale;
		tempScale.y = topLeft.y - bottomLeft.y;
		leftBorder.localScale = tempScale;
		
		if(rightBorder == null)
		{
			GameObject rightBorderGO = GameObject.Instantiate(borderCubePrefab) as GameObject;
			rightBorderGO.name = "right screen border collision cube";
			rightBorder = rightBorderGO.transform;
			rightBorder.parent = transform;
		}
		rightBorder.position = new Vector3(bottomRight.x + 0.5f, ((topRight.y + bottomRight.y) * 0.5f), bottomRight.z);
		tempScale = rightBorder.localScale;
		tempScale.y = topRight.y - bottomRight.y;
		rightBorder.localScale = tempScale;

		if(bottomBorder == null)
		{
			GameObject bottomBorderGO = GameObject.Instantiate(borderCubePrefab) as GameObject;
			bottomBorderGO.name = "bottom screen border collision cube";
			bottomBorder = bottomBorderGO.transform;
			bottomBorder.parent = transform;

		}
		bottomBorder.position = new Vector3(((bottomLeft.x + bottomRight.x) * 0.5f), bottomLeft.y - 0.5f, bottomLeft.z);
		tempScale = bottomBorder.localScale;
		tempScale.x = bottomRight.x - bottomLeft.x;
		bottomBorder.localScale = tempScale;

		if(topBorder == null)
		{
			GameObject topBorderGO = GameObject.Instantiate(borderCubePrefab) as GameObject;
			topBorderGO.name = "top screen border collision cube";
			topBorder = topBorderGO.transform;
			topBorder.parent = transform;
		}
		topBorder.position = new Vector3(((topLeft.x + topRight.x) * 0.5f), topLeft.y + 0.5f, topLeft.z);
		tempScale = topBorder.localScale;
		tempScale.x = topRight.x - topLeft.x;
		topBorder.localScale = tempScale;
	}
	
	void Update ()
	{
		if(!Mathf.Approximately(screenRatio,GetScreenRatio()))
		{
			CalculateColliders();
			screenRatio = GetScreenRatio();
		}
	}
}
