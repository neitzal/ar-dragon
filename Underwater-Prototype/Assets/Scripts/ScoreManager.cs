using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ScoreManager {

	public int score = 0;
	public int[] highScore;
	int highest;
	public Text ui;
	public int scoreForNextLevel = 500;
	GameManagement gm;

	public ScoreManager(Text text, GameManagement gameManagement) {
		gm = gameManagement;
		score = 0;
		ui = text;
		updateScore ();
	}

	public void AddScore(int value) {
		score += value;
		updateScore ();
		if (score >= scoreForNextLevel) {
			gm.ShowInfoScreen ("Herzlichen Glückwunsch, du hast genügend Diamanten gesammelt um ins nächste Level zu gelangen. Finde das Portal um zum nächsten Abenteuer aufzubrechen!");
			gm.showPortal ();
		}
	}

	void updateScore() {
		//Debug.Log(score.ToString());
		ui.text = score.ToString ();
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
