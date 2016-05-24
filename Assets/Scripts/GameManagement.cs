using UnityEngine;
using System.Collections;

public class GameManagement : MonoBehaviour {

	public GameObject floorImageTarget;
	public GameObject snakePrefab;
	public GameObject resetButton;
	public GameObject buttonLeft;
	public GameObject buttonRight;

	private GameObject snake;
	private SnakeMovement snakeMovement;

	public void ResetGame() {
		if (snake != null) {
			Destroy(snake);
		}
		snake = (GameObject) Instantiate(snakePrefab, Vector3.zero, Quaternion.identity);
		snake.transform.parent = floorImageTarget.transform;
		snakeMovement = snake.GetComponent<SnakeMovement>();
	}

	public void StartTurningLeft() {
		snakeMovement.StartTurningLeft();
	}

	public void StopTurningLeft() {
		snakeMovement.StopTurningLeft();
	}

	public void StartTurningRight() {
		snakeMovement.StartTurningRight();
	}

	public void StopTurningRight() {
		snakeMovement.StopTurningRight();
	}

	public void GameOver() {
		resetButton.SetActive(true);
	}


	void Update() {
		if (Input.GetKeyDown(KeyCode.S)) {
			ResetGame();
			resetButton.SetActive(false);
		}
	}
}
