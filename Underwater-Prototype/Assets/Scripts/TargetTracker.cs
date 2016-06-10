using UnityEngine;
using System.Collections;

public class TargetTracker : MonoBehaviour {

	public Transform trackedObject;
	public float spring = 10;
	public float damp = 5;

	private Vector3 targetDisplacement;
	private Rigidbody rb;



	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		targetDisplacement = trackedObject.position - transform.position;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		Vector3 currentDisplacement = trackedObject.position - transform.position;

		Vector3 discrepancy =  currentDisplacement - targetDisplacement;

		rb.AddForce( spring * discrepancy );
		rb.AddForce( - damp * rb.velocity);
	}
}
