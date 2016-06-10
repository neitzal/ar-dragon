using UnityEngine;
using System.Collections;

public class FoodBehavior : MonoBehaviour {
		
	public ParticleSystem particleSystem;

	void OnCollisionEnter(Collision collision) {
		if (collision.rigidbody.CompareTag("DragonHead")) {

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
