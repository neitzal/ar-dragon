using UnityEngine;
using System.Collections;

public class GameManagement : MonoBehaviour {

	public GameObject foodPrefab;
	ScoreManager scoreManager;
	// Use this for initialization
	void Start () {
		Debug.Log ("game started");
		scoreManager = new ScoreManager ();
		Instantiate (foodPrefab);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void FoodEaten() {
		scoreManager.AddScore (10);
		Instantiate (foodPrefab);
	}
}
