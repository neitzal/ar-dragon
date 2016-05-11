using UnityEngine;
using System.Collections;

public class GameManagement : MonoBehaviour {

	public GameObject snakePrefab;
	public GameObject resetButton;

	private GameObject snake;


	public void ResetGame() {
		if (snake != null) {
			Destroy(snake);
		}
		snake = (GameObject) Instantiate(snakePrefab, Vector3.zero, Quaternion.identity);
	}

	public void GameOver() {
		resetButton.SetActive(true);
	}

}
