using UnityEngine;
using System.Collections;

public class EndSequence : MonoBehaviour {

	public string sceneToChangeTo;

	void OnCollisionEnter(Collision col){

		if (col.gameObject.name == "CubeCollider") {

			Application.LoadLevel (sceneToChangeTo);

		}

	}
}
