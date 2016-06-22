using UnityEngine;
using System.Collections;

public class GameManagement : MonoBehaviour {

	public GameObject foodPrefab;
	// Use this for initialization
	void Start () {
		Debug.Log ("game started");
		Instantiate (foodPrefab);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void FoodEaten() {
		Instantiate (foodPrefab);
	}
}
