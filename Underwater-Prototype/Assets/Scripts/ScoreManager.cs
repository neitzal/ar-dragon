using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScoreManager {

	public int score = 0;
	public int[] highScore;
	int highest;

	public void AddScore(int value) {
		score += value;
		updateScore ();
	}

	void updateScore() {
		//scoreUI.text = score.ToString();
		Debug.Log(score.ToString());
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
