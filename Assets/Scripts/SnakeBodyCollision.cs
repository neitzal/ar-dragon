using UnityEngine;
using System.Collections;

public class SnakeBodyCollision : MonoBehaviour {
	public bool firstSegment = true;
	void OnTriggerEnter(Collider other) {
		if (firstSegment) {
			return;
		}
		if (other.transform.parent != null) {
			SnakeMovement sm = other.transform.parent.GetComponent<SnakeMovement>();
			if (sm != null) {
				sm.ObstacleHit();
			}	
		}

	}
}
