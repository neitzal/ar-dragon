using UnityEngine;
using System.Collections;

public class FoodBehavior : MonoBehaviour {
		
	public ParticleSystem particleSystem;

	void OnCollisionEnter(Collision collision) {
		if (collision.rigidbody == null) {
			Debug.Log("Something collided with food, without having a rigid body component!");
		} else if (collision.rigidbody.CompareTag("DragonHead")) {

			collision.rigidbody.GetComponent<FoodEatWave>().InitiateWave();

			collision.rigidbody.GetComponent<HeadMovement>().CreateSegments(5);

			GetComponent<Animator>().SetTrigger("Disappear");
			particleSystem.Play();
		}
	}

	void Update() {
		if (transform.localScale.x <= 0.001f) {
			Destroy(this.gameObject);
		}
	}
}
