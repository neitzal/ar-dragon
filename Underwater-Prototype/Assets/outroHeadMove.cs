using UnityEngine;
using System.Collections;

public class outroHeadMove : MonoBehaviour {
	private Rigidbody head; 
	public float Amplitude = 45f; 
	public float frequency =3f; 
	public float forwardSpeed = 117.86f; 
	public float endPointz = 150f;
	// Use this for initialization
	void Awake(){
		head = GetComponent<Rigidbody> (); 
	}
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var f = Mathf.Cos(Time.time * frequency);

		head.AddTorque (Amplitude*transform.up*f);
		head.AddForce (transform.forward*forwardSpeed); 

		if (transform.position.z > endPointz) {
			Application.LoadLevel ("menuscene");
		}
	}
}
