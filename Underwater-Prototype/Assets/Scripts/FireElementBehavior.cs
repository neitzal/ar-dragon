using UnityEngine;
using System.Collections;

public class FireElementBehavior : MonoBehaviour {

	public float rotationSpeedRange = 3000f;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		rb.angularVelocity = new Vector3(
			Random.Range(-rotationSpeedRange, rotationSpeedRange), 
			Random.Range(-rotationSpeedRange, rotationSpeedRange), 
			Random.Range(-rotationSpeedRange, rotationSpeedRange));
	}

	void OnCollisionEnter(Collision collision) {
		Destroy(this.gameObject);
	}
}
