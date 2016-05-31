using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManagement : MonoBehaviour {

	public GameObject floorImageTarget;
	public GameObject snakePrefab;
	public GameObject resetButton;
	public GameObject buttonLeft;
	public GameObject buttonRight;
	public Text scoreUI;

	private GameObject snake;
	private SnakeMovement snakeMovement;
	public int score = 0;

	public void Start() {
		score = 0;
		scoreUI = GameObject.Find ("Canvas/HUD/Score").GetComponent<Text>();
		updateScore ();
	}

	public void ResetGame() {
		score = 0;
		updateScore ();
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
	
	public void AddScore(int value) {
		score += value;
		updateScore ();
	}
	void updateScore() {
		scoreUI.text = score.ToString();
	}
}
