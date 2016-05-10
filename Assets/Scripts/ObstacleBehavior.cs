using UnityEngine;
using System.Collections;

public class ObstacleBehavior : MonoBehaviour {
	void OnTriggerEnter(Collider other) {
		if (other.transform.parent != null) {
			SnakeMovement sm = other.transform.parent.GetComponent<SnakeMovement>();
			if (sm != null) {
				sm.ObstacleHit();	
			}
		}
	}
}
