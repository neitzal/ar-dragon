using UnityEngine;
using System.Collections;

public class CollisionResponse : MonoBehaviour {

	void OnCollisionEnter(Collision collision) {
		if (collision.rigidbody != null) {
			ContactPoint contactA = collision.contacts[0];
			collision.rigidbody.velocity = -3 * contactA.normal;

			if (collision.gameObject.CompareTag("DragonHead")) {
				collision.gameObject.GetComponent<SnakeHealth>().ApplyDamage(20);
			}
		}
	}
}
