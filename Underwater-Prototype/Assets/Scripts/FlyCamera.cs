using UnityEngine;
using System.Collections;

public class FlyCamera : MonoBehaviour {
	private Camera cam; 
	public float turnSpeed = 100f; 
	public Transform target;
	public float forwardSpeed = 10f;
	public float minDistance = 3f; 
	public float waterLevel = 4f; 
	void Awake(){
		cam = GetComponentInChildren<Camera> (); 
	}
	// Update is called once per frame
	void Update () {
		// cam.transform.localPosition = new Vector3 (); 

	
		transform.position = target.position;
		if (transform.position.y < waterLevel) {
			transform.position = new Vector3 (transform.position.x, waterLevel, transform.position.z);
		}
		if (cam.transform.localPosition.magnitude > minDistance) {
			cam.transform.localPosition -= cam.transform.localPosition.normalized * forwardSpeed * Time.smoothDeltaTime; 
			transform.Rotate (-Vector3.up * Time.smoothDeltaTime * turnSpeed); 
		}



	}
}
