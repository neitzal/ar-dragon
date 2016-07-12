using UnityEngine;
using System.Collections;

public class OnFeuerDamage : MonoBehaviour {
	private int count = 0; 
	private Color color; 

//	private Color[] colors = {new Color(1f, 0, 0), new Color(0.5f, 0, 0), new Color(0.25f, 0, 0), new Color(0.125f, 0, 0) };

	void OnCollisionEnter(Collision col){
		color = GetComponent<MeshRenderer> ().material.color; 
		Debug.Log (color); 

		if (col.gameObject.CompareTag ("Feuer")) {
			count++;
			gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(color.r/2, 0, 0)); 
			gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(color.r/2, 0, 0)); 
		}
		/*if (count == 1) {
			Debug.Log ("hit 1 time"); 

		}else if(count == 2) {
			Debug.Log ("hit 2 time"); 
			gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(0.5f, 0, 0)); 
			gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0.5f, 0, 0)); 
		}else if(count == 3){
			Debug.Log ("hit 3 time");
			gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(0.25f, 0, 0));
			gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0.25f, 0, 0)); 
		}else if(count == 4){
			Debug.Log ("hit 4 time");
			gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", new Color(0.125f, 0, 0));
			gameObject.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor", new Color(0.125f, 0, 0)); 
		}else */
		if(count > 4){
			Destroy (gameObject); 
		}
	}
}
