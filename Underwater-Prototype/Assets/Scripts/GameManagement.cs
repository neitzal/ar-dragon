using UnityEngine;
using System.Collections;

public class GameManagement : MonoBehaviour {

	private HeadMovement headMovement;
	private Animator gameOverTextAnim;


	void Start() {
		var headMovements = GameObject.FindObjectsOfType<HeadMovement>();
		if (headMovements.Length == 0) {
			throw new UnityException("No HeadMovement Script found in Scene!");
		} else if (headMovements.Length > 1) {
			throw new UnityException("Currently multiple Instances of HeadMovement are not supported!");
		}

		headMovement = headMovements[0];

		gameOverTextAnim = GameObject.Find("GameOverText").GetComponent<Animator>();
	}

	public void OnSnakeDead() {
		Debug.Log("Game over!");
		gameOverTextAnim.SetTrigger("GameOver");
		headMovement.Playing = false;

	}
		
	public void StartTurningRight() {
		headMovement.StartTurningRight();
	}

	public void StopTurningRight() {
		headMovement.StopTurningRight();
	}

	public void StartTurningLeft() {
		headMovement.StartTurningLeft();
	}

	public void StopTurningLeft() {
		headMovement.StopTurningLeft();
	}

}
