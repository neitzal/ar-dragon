using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {
	public float radius = 7.0f; 
	public float scale = 300.0f;


	// Update is called once per frame
	void Update () {
		float angle = (Input.mousePosition.x - Screen.width/2)/scale;
		float y = (Input.mousePosition.y - Screen.height/2)/100.0f;
		transform.position =  new Vector3(radius * Mathf.Sin(angle), 7 + y, radius * Mathf.Cos(angle));
		transform.LookAt(Vector3.zero);
	}
}
