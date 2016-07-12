using UnityEngine;
using System.Collections;

public class CollisionResponse : MonoBehaviour {

	public float damage = 20;
	public float recoil = 3;
	public GameObject hurtsound;


	void OnCollisionEnter(Collision collision) {
		if (collision.rigidbody != null) {	
			ContactPoint contactA = collision.contacts[0];
			collision.rigidbody.velocity = -recoil * contactA.normal;

			if (collision.gameObject.CompareTag("DragonHead")) {
				Debug.Log ("snake hurt 20");
				collision.gameObject.GetComponent<SnakeHealth>().ApplyDamage(damage);

			}
		}
	}

	void OnCollisionStay(Collision col){
		if (col.rigidbody != null) {
			if (col.gameObject.CompareTag ("DragonHead")) {
//				hurtsound = GameObject.Find ("HurtSound");
//				hurtsound.GetComponent<AudioSource> ().Play ();
			}
		}

	}
}
