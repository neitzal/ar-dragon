using UnityEngine;
using System.Collections;

public class SegmentWiggle : MonoBehaviour {

	public float period = 1.0f; // Seconds
	public float offset = 0.0f;

	public float forwardForce = 1;

	private Rigidbody rb;

	private float wiggleForce = 0;

	void Awake() {
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate () {
		rb.AddForce(wiggleForce * Mathf.Sin(offset + Time.time * 2 * Mathf.PI / period) * Vector3.Cross(Vector3.up, transform.forward));	
		rb.AddForce(forwardForce * transform.forward);
	}

	public void setWiggleForce(float wiggleForce) {
		this.wiggleForce = wiggleForce;
	}
}
