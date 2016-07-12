using UnityEngine;
using System.Collections;

public class outroHeadMove : MonoBehaviour {
	private Rigidbody head; 
	public float Amplitude = 45f; 
	public float frequency =3f; 
	public float forwardSpeed = 117.86f; 
	// Use this for initialization
	void Awake(){
		head = GetComponent<Rigidbody> (); 
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var f = Mathf.Cos(Time.time * frequency);
		//if (Mathf.Abs(f) < 0.9f) {
		//	f = 0;
		//}
		//f = f * f * f;
		head.AddTorque (Amplitude*transform.up*f);
		head.AddForce (transform.forward*forwardSpeed); 
	}
}
