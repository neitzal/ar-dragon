using UnityEngine;
using System.Collections;

public class HeadCollision : MonoBehaviour {

	GameManagement gameManagement;

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
		if (other.CompareTag ("Obstacle")) {
			Debug.Log ("obstacle hit");
			gameManagement.ReduceHealth (10);
		}
	}
}
