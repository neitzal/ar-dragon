using UnityEngine;
using System.Collections;

public class GameManagement : MonoBehaviour {

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

}
