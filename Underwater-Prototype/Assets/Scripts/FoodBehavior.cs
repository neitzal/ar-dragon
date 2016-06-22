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

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("DragonHead")) {
			Debug.Log ("collision with snakehead");
			other.GetComponent<FoodEatWave> ().InitiateWave ();

			other.GetComponent<HeadMovement> ().CreateSegments (5);

			GetComponent<Animator> ().SetTrigger ("Disappear");
			particleSystem.Play ();

			gameManagement.FoodEaten ();
		}
	}

	void Update() {
		if (transform.localScale.x <= 0.001f) {
			Destroy(this.gameObject);
		}
	}
}
