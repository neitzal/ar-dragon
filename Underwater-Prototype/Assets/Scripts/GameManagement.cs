using UnityEngine;
using System.Collections;

public class GameManagement : MonoBehaviour {

	public GameObject foodPrefab;
	ScoreManager scoreManager;
	public GameObject damageUI;
	int health;
	// Use this for initialization
	void Start () {
		Debug.Log ("game started");
		health = 100;
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
	public void ReduceHealth(int value) {
		health -= value;
		StartCoroutine (flashDamage ());
		Debug.Log ("health: " + health.ToString ());
	}

	IEnumerator flashDamage() {
		damageUI.SetActive (true);
		yield return new WaitForEndOfFrame();
		damageUI.SetActive(false);
	}
}
