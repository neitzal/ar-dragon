using UnityEngine;
using System.Collections;



public class FoodBehavior : MonoBehaviour {

	public enum FoodType { 
		Regular, 
		Diamond 
	}
		
	public GameObject gameManagementComponent;
	public FoodType foodType;
	private GameManagement gameManager;

	void Start() {
		gameManager = gameManagementComponent.GetComponent<GameManagement> ();
	}


	void OnCollisionEnter(Collision collision) {
		if (collision.rigidbody == null) {
			Debug.Log ("Something collided with food, without having a rigid body component!");
		} 
		else if (collision.rigidbody.CompareTag ("DragonHead")) {
			switch(foodType) {

				case FoodType.Regular: {
					Debug.Log ("collision dragonhead-food");

					collision.rigidbody.GetComponent<FoodEatWave> ().InitiateWave ();

					// TODO: if body is near level boundary segment gets created outside of level

					// TODO: sometimes segment animation get broken and wiggling looks not very smooth
					collision.rigidbody.GetComponent<HeadMovement> ().CreateSegments (2);

					gameManager.scoreManager.AddScore (100);
					break;
				}

				case FoodType.Diamond: {
					gameManager.ShowInfoScreen ("Gratulation, Du hast einen Diamanten eingesammelt. Der Drache kann jetzt Feuer spucken!");
					Destroy(gameObject);
					gameManager.scoreManager.AddScore(200);
					gameManager.SetFireBreathing(true);
					break;
				}
			}
		}
	}
}
