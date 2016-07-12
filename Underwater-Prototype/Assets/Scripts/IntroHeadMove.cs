using UnityEngine;
using System.Collections;

public class IntroHeadMove : MonoBehaviour {

	private Rigidbody rb; 
	public float torqueAmplitude = 10f;
	public float forwardForce = 20f;
	public float frequency = 0.5f; 
	void Awake(){
		rb = GetComponent<Rigidbody> (); 
	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		rb.AddTorque (torqueAmplitude * Mathf.Cos (Time.time * frequency) * transform.up);
		rb.AddForce(forwardForce * transform.forward);
	} 
}
