using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GameManagement : MonoBehaviour {

	public GameObject floorImageTarget;
	public GameObject snakePrefab;
	public GameObject resetButton;
	public GameObject buttonLeft;
	public GameObject buttonRight;
	public Text scoreUI;
	public Text highScoreUI;

	private GameObject snake;
	private SnakeMovement snakeMovement;
	public int score = 0;
	public int[] highScore;
	int highest;

	// animation variables
	Animator anim;

	void Awake() {
		anim = GetComponent<Animator> ();
		//resetButton = GetComponent<GameObject> ("ResetButton");
	}

	public void Start() {
		score = 0;
		scoreUI = GameObject.Find ("Canvas/HUD/Score").GetComponent<Text>();
		highScoreUI = GameObject.Find ("Canvas/HUD/HighScore").GetComponent<Text>();
		updateScore ();
		updateHighScore ();
	}

	public void ResetGame() {
		//anim.ResetTrigger ("GameOver");
		resetButton.SetActive (false);
		score = 0;
		updateScore ();
		updateHighScore ();
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
		resetButton.SetActive (true);
		anim.SetTrigger ("GameOver");
		setHighScore (score);
	}
	
	public void AddScore(int value) {
		score += value;
		updateScore ();
	}
	void updateScore() {
		scoreUI.text = score.ToString();
	}
	List<int> getHighScores() {
		List<int> scores = new List<int>();
		for (int i = 0; i <= 4; i++) {
			scores.Add(PlayerPrefs.GetInt ("score-" + i.ToString ()));
		}
		scores.Sort ();
		return scores;
	}
	void setHighScore(int score) {
		List<int> scores = getHighScores ();
		scores.Add (score);
		scores.Sort ();
		scores.RemoveAt (0);
		for (int i = 0; i <= 4; i++) {
			PlayerPrefs.SetInt ("score-" + i.ToString (), scores[i]);
		}
	}
	void updateHighScore() {
		string scoreString = "";
		List<int> scores = getHighScores ();
		foreach (int element in scores) {
			scoreString += element.ToString () + "\n";
		}
		highScoreUI.text = "Highscores: \n" + scoreString;
	}
}
