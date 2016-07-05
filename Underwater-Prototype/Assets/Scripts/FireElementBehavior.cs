using UnityEngine;
using System.Collections;

public class FireElementBehavior : MonoBehaviour {

	//TODO: for performance optimization make everything to particles

	public float rotationSpeedRange = 3000f;
	private Rigidbody rb;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody>();
		rb.angularVelocity = new Vector3(
			Random.Range(-rotationSpeedRange, rotationSpeedRange), 
			Random.Range(-rotationSpeedRange, rotationSpeedRange), 
			Random.Range(-rotationSpeedRange, rotationSpeedRange));
		Destroy (this.gameObject, 1.0f);
	}

	void OnCollisionEnter(Collision collision) {
		Destroy(this.gameObject);
	}
}
