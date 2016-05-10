using UnityEngine;
using System.Collections;

public class FoodRepositioning : MonoBehaviour {
	public enum FoodType {
		Elongation, SlowDown
	}

	public FoodType type;

	void OnTriggerEnter(Collider other) {
		if (other.transform.parent != null) {
			SnakeMovement sm = other.transform.parent.GetComponent<SnakeMovement>();
			if (sm != null) {
				if (type == FoodType.Elongation) {
					sm.FoodEaten();
				} else if (type == FoodType.SlowDown) {
					sm.SlowDown();
				}
			}
		}
		Reposition();
	}

	void OnTriggerStay(Collider other) {
		if (other.CompareTag("obstacle")) {
			Reposition();
		}
	}

	void Reposition() {
		transform.position = new Vector3(
			Random.Range(-5, 5), 
			transform.position.y, 
			Random.Range(-5, 5));
	}
}
