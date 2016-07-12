using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class HeadMovement : MonoBehaviour {
	public float forwardForce = 20.0f;
	public float wiggleForce = 0.8f;
	public float forwardForcePeriod = 1.0f;
	public float forwardForceAmplitude = 10.0f;
	public float torque = 3.0f;
	public float angularVel = 180.0f;
	public float upwardTiltEqualization = 0.5f;
	public float axialTiltEqualization = 1.0f;
	public float collisionHeadTilt = 20f;

	public int nSegments = 10;
	public float distanceBetweenSegments = 0.2f;

	public GameObject segmentPrefab;
	public bool Playing { get; set; }

	private Rigidbody rb;
	public List<Rigidbody> segmentBodies;

	private float turnInput = 0; // Number between -1 (left) and 1 (right), specifies how head should be turned
	private bool enteredPortal = false;

	void Awake () {
		rb = GetComponent<Rigidbody>();
		segmentBodies = new List<Rigidbody>();
		CreateSegments(nSegments);
		Playing = true;

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

			segment.transform.SetParent(transform.parent.transform);

			segmentBodies.Add(segment.GetComponent<Rigidbody>());
			var segmentWiggle = segment.GetComponent<SegmentWiggle>();
			segmentWiggle.offset = (float) segmentBodies.Count / 2.0f;
			segmentWiggle.setWiggleForce(wiggleForce * Mathf.Max(0, (1.0f - ((float) segmentBodies.Count / n))));
		}	
	}

	public void StartTurningRight() {
		turnInput = 1;
	}

	public void StopTurningRight() {
		if (turnInput > 0) {
			turnInput = 0;
		}
	}

	public void StartTurningLeft() {
		turnInput = -1;
	}

	public void StopTurningLeft() {
		if (turnInput < 0) {
			turnInput = 0;
		}
	}

	void Update() {
//		if (!Playing) {
//			return;
//		}

		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			turnInput = -1;
		} 
		if (Input.GetKeyUp(KeyCode.LeftArrow)) {
			if (turnInput < 0) {
				turnInput = 0;
			}
		}

		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			turnInput = 1;
		} 
		if (Input.GetKeyUp(KeyCode.RightArrow)) {
			if (turnInput > 0) {
				turnInput = 0;
			}
		}


	}

	void FixedUpdate () {
		if (enteredPortal) {
			rb.AddForce(10* Vector3.down);
			return;
		}

		if (!Playing) {
			rb.constraints = RigidbodyConstraints.None;
			rb.useGravity = true;
			rb.drag = 0;
			rb.angularDrag = 0;
			return;
		}

		Vector3 direction = transform.forward;
//		direction.y = 0;
		rb.AddForce((forwardForce + forwardForceAmplitude*Mathf.Sin(2*Mathf.PI * Time.time / forwardForcePeriod)) * direction.normalized);

		if (Mathf.Sign(turnInput) * rb.angularVelocity.y < angularVel) {
			rb.AddTorque(turnInput * torque * Vector3.up);	
		}

		// Smoothly restore horizontal head tilt
		float upwardTilt = transform.eulerAngles.x;
		if (upwardTilt > 180) upwardTilt -= 360;
		rb.AddTorque(-upwardTiltEqualization * upwardTilt * transform.right);


		float axialTilt = transform.eulerAngles.z;
		if (axialTilt > 180) axialTilt -= 360;
		rb.AddTorque(-axialTiltEqualization * axialTilt * transform.forward);
	}


	public void OnEnterPortal() {
		enteredPortal = true;
		rb.constraints = RigidbodyConstraints.None;

		rb.drag = 0;
		rb.angularDrag = 0;
		gameObject.layer = 0;

		foreach (var segment in segmentBodies) {
			segment.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
			segment.gameObject.layer = 0;
		}
	}

	void OnCollisionStay(Collision collision) {
		if (collision.collider.gameObject.layer == LayerMask.NameToLayer("StaticObstacles")) {
			rb.AddTorque(-collisionHeadTilt * transform.right);
		}
	}
}
