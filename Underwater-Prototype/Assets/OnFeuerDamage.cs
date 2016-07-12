using UnityEngine;
using System.Collections;

public class OnFeuerDamage : MonoBehaviour {
	private int count = 0; 
	private Color color; 

	void OnCollisionEnter(Collision col){
		color = GetComponent<MeshRenderer> ().material.color; 

		if (col.gameObject.CompareTag ("Feuer")) {
			count++;
			gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(color.r/2, 0, 0)); 
			gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(color.r/2, 0, 0)); 
		}

		if(count > 4){
			Destroy (gameObject); 
		}
	}
}
