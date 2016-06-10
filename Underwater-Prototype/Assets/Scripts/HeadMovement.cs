using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HeadMovement : MonoBehaviour {



	public float forwardForce = 20.0f;
	public float wiggleForce = 0.8f;
	public float forwardForcePeriod = 1.0f;
	public float forwardForceAmplitude = 10.0f;
	public float torque = 3.0f;
	public float angularVel = 180.0f;
	public int nSegments = 10;
	public float distanceBetweenSegments = 0.2f;
	public GameObject segmentPrefab;

	private Rigidbody rb;
	public List<Rigidbody> segmentBodies;


	void Awake () {
		rb = GetComponent<Rigidbody>();
		segmentBodies = new List<Rigidbody>();
		CreateSegments(nSegments);

	}

	public void CreateSegments(int n) {
		for (int i = 0; i < n; i++) {
			var previousTransform = segmentBodies.Count > 0 ? segmentBodies[segmentBodies.Count - 1].transform : this.transform;
			var segment = (GameObject) Instantiate(
				segmentPrefab, 
				previousTransform.position - distanceBetweenSegments * previousTransform.forward, 
				Quaternion.Euler(previousTransform.forward));

			CharacterJoint joint = segment.GetComponent<CharacterJoint>();
			if (segmentBodies.Count == 0) {
				joint.connectedBody = this.GetComponent<Rigidbody>();
			} else {
				joint.connectedBody = segmentBodies[segmentBodies.Count - 1];
			}

			segment.transform.SetParent(GameObject.Find("Snake").transform);

			segmentBodies.Add(segment.GetComponent<Rigidbody>());
			var segmentWiggle = segment.GetComponent<SegmentWiggle>();
			segmentWiggle.offset = (float) segmentBodies.Count / 2.0f;
			segmentWiggle.setWiggleForce(wiggleForce * Mathf.Max(0, (1.0f - ((float) segmentBodies.Count / n))));
		}	
	}
	
	void FixedUpdate () {
		Vector3 direction = transform.forward;
		direction.y = 0;
		rb.AddForce((forwardForce + forwardForceAmplitude*Mathf.Sin(2*Mathf.PI * Time.time / forwardForcePeriod)) * direction.normalized);

		if (Input.GetKey(KeyCode.LeftArrow)) {
			if (rb.angularVelocity.y > (-angularVel)) {
				rb.AddTorque(-torque * Vector3.up);	
			}
		}
		if (Input.GetKey(KeyCode.RightArrow)) {
//			float currentTorque = torque * (angularVel - rb.angularVelocity.y);
			if (rb.angularVelocity.y < angularVel) {
				rb.AddTorque(torque * Vector3.up);	
			}
		}
	}
}
