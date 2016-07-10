using UnityEngine;
using System.Collections;

public class CollisionResponse : MonoBehaviour {

	public GameObject hurtsound;

	void OnCollisionEnter(Collision collision) {
		if (collision.rigidbody != null) {
			ContactPoint contactA = collision.contacts[0];
			collision.rigidbody.velocity = -3 * contactA.normal;

			if (collision.gameObject.CompareTag("DragonHead")) {
				collision.gameObject.GetComponent<SnakeHealth>().ApplyDamage(20);

			}
		}
	}

	void OnCollisionStay(Collision col){
		if (col.rigidbody != null) {
			if (col.gameObject.CompareTag ("DragonHead")) {
				hurtsound = GameObject.Find ("HurtSound");
				hurtsound.GetComponent<AudioSource> ().Play ();
			}
		}

	}
}
