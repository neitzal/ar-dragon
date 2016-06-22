using UnityEngine;
using System.Collections;

public class FoodPositioning : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Reposition ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider other) {
		if (other.CompareTag("obstacle")) {
			Reposition();
		}
	}

	void Reposition() {
		transform.position = new Vector3(
			Random.Range(-5, 5), 
			transform.position.y, 
			Random.Range(-5, 5));
	}
}
