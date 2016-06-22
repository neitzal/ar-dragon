using UnityEngine;
using System.Collections;

public class FoodBehavior : MonoBehaviour {
		
	public ParticleSystem particleSystem;
	public GameManagement gameManagement;

	void Start() {
		{
			GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
			if (gameControllerObject != null)
			{
				gameManagement = gameControllerObject.GetComponent <GameManagement>();
			}
			if (gameManagement == null)
			{
				Debug.Log ("Cannot find 'GameController' script");
			}
		}
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.rigidbody.CompareTag("DragonHead")) {

			collision.rigidbody.GetComponent<FoodEatWave>().InitiateWave();

			collision.rigidbody.GetComponent<HeadMovement>().CreateSegments(5);

			GetComponent<Animator>().SetTrigger("Disappear");
			particleSystem.Play();

			gameManagement.FoodEaten ();
		}
	}

	void Update() {
		if (transform.localScale.x <= 0.001f) {
			Destroy(this.gameObject);
		}
	}
}
