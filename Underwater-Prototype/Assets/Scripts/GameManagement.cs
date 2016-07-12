using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour {

	private HeadMovement headMovement;
	private Animator gameOverTextAnim;

	private AudioSource audiosource { get { return GetComponent<AudioSource> (); } }
	public AudioClip gameoversound;
	public ScoreManager scoreManager;
	public Text scoreUI;
	public GameObject resetButton;

	public Vector3 gravity = new Vector3(0, -9.81f, 0);

	void Start() {
		var headMovements = GameObject.FindObjectsOfType<HeadMovement>();
		if (headMovements.Length == 0) {
			throw new UnityException("No HeadMovement Script found in Scene!");
		} else if (headMovements.Length > 1) {
			throw new UnityException("Currently multiple Instances of HeadMovement are not supported!");
		}

		headMovement = headMovements[0];

		var gameOverText = GameObject.Find("GameOverText");
		if (gameOverText != null) {
			gameOverTextAnim = gameOverText.GetComponent<Animator>();	
		} else {
			Debug.LogError("No GameObject called 'GameOverText' was found!");
		}

		Physics.gravity = gravity;

		gameObject.AddComponent<AudioSource> ();
		audiosource.clip = gameoversound;
		audiosource.playOnAwake = false;
		audiosource.volume = (float)0.4;

		// setup scoreManager and UI
		scoreManager = new ScoreManager (scoreUI);

		// disable resetButton
		resetButton.SetActive(false);

	}

	public void ResetGame() {
		Application.LoadLevel(Application.loadedLevel);
	}

	public void OnSnakeDead() {
		Debug.Log("Game over!");
		gameOverTextAnim.SetTrigger("GameOver");
		headMovement.Playing = false;
		PlayGameOverSound();
		resetButton.SetActive (true);
	}
		
	void PlayGameOverSound(){
		audiosource.Play ();
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
