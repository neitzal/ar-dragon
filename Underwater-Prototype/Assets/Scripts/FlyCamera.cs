using UnityEngine;
using System.Collections;

public class FlyCamera : MonoBehaviour {
	private Camera cam; 
	public float turnSpeed = 100f; 
	public Transform target;
	public float forwardSpeed = 10f; 

	void Awake(){
		cam = GetComponentInChildren<Camera> (); 
	}
	// Update is called once per frame
	void Update () {
		// cam.transform.localPosition = new Vector3 (); 
		transform.position = target.position;
		transform.Rotate(-Vector3.up * Time.smoothDeltaTime * turnSpeed);  
		cam.transform.localPosition -= cam.transform.localPosition.normalized * forwardSpeed * Time.smoothDeltaTime; //(transform.position - cam.transform.position).normalized * forwardSpeed * Time.smoothDeltaTime;
		// cam.transform.Translate((transform.position-cam.transform.position).normalized*forwardSpeed*Time.smoothDeltaTime); 
	}
}
