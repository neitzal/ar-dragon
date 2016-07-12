using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScoreManager {

	public int score = 0;
	public int[] highScore;
	int highest;
	public GameObject scoreUI;
	public int scoreForNextLevel = 600;
	GameManagement gm;
	private bool isLevelUp;

	public ScoreManager(GameObject scoreUI, GameManagement gameManagement) {
		gm = gameManagement;
		score = 0;
		this.scoreUI = scoreUI;
		//updateScore ();
	}

	public void AddScore(int value) {
		score += value;
		updateScore ();
		if (score >= scoreForNextLevel && !isLevelUp) {
			gm.ShowInfoScreen ("Herzlichen Glückwunsch, du hast genügend Diamanten gesammelt um ins nächste Level zu gelangen. Finde das Portal um zum nächsten Abenteuer aufzubrechen!");
			gm.showPortal ();
			isLevelUp = true;
		}
	}

	void updateScore() {
		scoreUI.GetComponent<Text> ().text = score.ToString ();
		scoreUI.GetComponent<ScoreAnimation> ().TriggerScoreAnimation ();
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
		//highScoreUI.text = "Highscores: \n" + scoreString;
	}
}
