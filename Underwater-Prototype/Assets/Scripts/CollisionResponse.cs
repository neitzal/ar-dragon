﻿using UnityEngine;
using System.Collections;

public class CollisionResponse : MonoBehaviour {

	void OnCollisionEnter(Collision collision) {
		if (collision.rigidbody != null) {
//			collision.rigidbody.velocity = -2* collision.relativeVelocity;
			ContactPoint contactA = collision.contacts[0];
//			ContactPoint contactB = collision.contacts[1];
//			Debug.Log(collision.contacts.Length);
//			Debug.Log(contactA.normal);
//			Debug.Log(contactB.normal);

//			collision.rigidbody.

//			Debug.LogFormat("Point A: {0}", contactA.point);
//			Debug.LogFormat("Point B: {0}", contactB.point);
				
			collision.rigidbody.velocity = -3 * contactA.normal;

			if (collision.gameObject.CompareTag("DragonHead")) {
				collision.gameObject.GetComponent<SnakeHealth>().ApplyDamage(20);
			}
		}
	}
}
